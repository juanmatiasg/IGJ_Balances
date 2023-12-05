using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Utils;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Balances.Bussiness
{
    public class CaratulaBusiness : ICaratulaBusiness
    {

        protected IMongoCollection<Balance> _balance;
        protected IMapper _mapper;
        private ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CaratulaBusiness> _logger;


        public CaratulaBusiness(IMongoDbSettings _settings,
                                IMapper mapper,
                                ISessionService sessionService,
                                IBalanceBusiness balanceBusiness,
                                IEmailSenderService emailSenderService,
                                IWebHostEnvironment webHostEnvironment,
                                ILogger<CaratulaBusiness> logger)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balance = database.GetCollection<Balance>(_settings.Collection);
            _mapper = mapper;
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _emailSenderService = emailSenderService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public bool Delete(CaratulaDto modelo)
        {
            throw new NotImplementedException();
        }



        public ResponseDTO<Balance> Insert(CaratulaDto modelo)
        {
            ResponseDTO<Balance> respuesta = new ResponseDTO<Balance>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = new Balance();
                var balSerializado = JsonConvert.SerializeObject(balance);


                balance.Caratula = MapearCaratula(modelo);

                var rsp = _balance.InsertOneAsync(balance);

                var balanceDto = _mapper.Map<BalanceDto>(balance);

                var plantillahtml = CrearPlantillaInicioTramite(balanceDto);

                var email = CrearEmaiInicioTramite(balanceDto, plantillahtml);
                //var EmailRequest = _emailSenderService.EmaiInicioTramite(balanceDto);
                _emailSenderService.SendEmailAsync(email);

                // si inserto correctamente
                if (rsp != null)
                {
                    _sessionService.SetBalanceId(balance.Id);
                    //balance.Id = rsp.Id.ToString();
                    respuesta.IsSuccess = true;
                    respuesta.Message = "Caratula creada correctamente";
                    respuesta.Result = balance;

                }
                else
                    respuesta.Message = "No se Pudo Insertar";

                _logger.LogError($"No se pudo insertar el balance: \n {balSerializado} ");

            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"error al insertar balance: \n {ex.Message} ");


            }
            return respuesta;
        }

        public string CrearPlantillaInicioTramite(BalanceDto balance)
        {
            string PlantillaHTML = GetPlantillaHtml("PlantillaEmail.html");

            PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
            PlantillaHTML = PlantillaHTML.Replace("{{BalanceId}}", balance.Id);



            return PlantillaHTML;

        }

        private string GetPlantillaHtml(string plantilla)
        {
            var path = _webHostEnvironment.ContentRootPath + "/Plantillas";
            var Plantilla = path + "/" + plantilla;

            var PlantillaHTML = File.ReadAllText(Plantilla);
            return PlantillaHTML;
        }

        public MimeMessage CrearEmaiInicioTramite(BalanceDto balance, string html)
        {
            var mime = new MimeMessage()
            {
                //    To = balance.Caratula.Email,
                Subject = $"Presentacion Generada - {balance.Caratula.Entidad.RazonSocial} ",
                //  Body = html,

            };

            mime.To.Add(new MailboxAddress("", balance.Caratula.Email));

            var builder = new BodyBuilder();
            var pathImage = _webHostEnvironment.ContentRootPath + "/Plantillas/Imagenes";

            /* A G R E G AM O S   I M A G E N E S   H E A D E R */
            var imgIGJ = builder.LinkedResources.Add("igj.png", File.ReadAllBytes(pathImage + "/igj.png"));
            imgIGJ.ContentId = MimeUtils.GenerateMessageId();
            html = html.Replace("{{igjImage}}", imgIGJ.ContentId);

            var imgMIN = builder.LinkedResources.Add("ministerio.png", File.ReadAllBytes(pathImage + "/ministerio.png"));
            imgMIN.ContentId = MimeUtils.GenerateMessageId();
            html = html.Replace("{{MinImage}}", imgMIN.ContentId);



            builder.HtmlBody = html;



            mime.Body = builder.ToMessageBody();

            return mime;
        }

        private Caratula MapearCaratula(CaratulaDto modelo)
        {
            var caratula = new Caratula()
            {
                FechaDeCierre = modelo.FechaDeCierre,
                Email = modelo.Email,
                Entidad = modelo.Entidad,
                FechaInicio = modelo.FechaInicio,
                Fecha = DateTime.Now,
            };

            return caratula;
        }

        private CaratulaDto MapearCaratulaDto(Caratula modelo)
        {
            var caratuladto = new CaratulaDto()
            {
                FechaDeCierre = modelo.FechaDeCierre,
                Email = modelo.Email,
                Entidad = modelo.Entidad,
                FechaInicio = modelo.FechaInicio,
            };

            return caratuladto;
        }

        public IEnumerable<CaratulaDto> List()
        {
            throw new NotImplementedException();
        }

        public bool Update(CaratulaDto modelo)
        {
            throw new NotImplementedException();
        }








    }
}
