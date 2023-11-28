using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MimeKit.Utils;
using MongoDB.Driver;

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


        public CaratulaBusiness(IMongoDbSettings _settings,
                                IMapper mapper,
                                ISessionService sessionService,
                                IBalanceBusiness balanceBusiness,
                                IEmailSenderService emailSenderService,
                                IWebHostEnvironment webHostEnvironment)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balance = database.GetCollection<Balance>(_settings.Collection);
            _mapper = mapper;
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _emailSenderService = emailSenderService;
            _webHostEnvironment = webHostEnvironment;
        }

        public bool Delete(CaratulaDto modelo)
        {
            throw new NotImplementedException();
        }

        //public ResponseDTO<CaratulaDto> GetById(string id)
        //{
        //    var respuesta = new ResponseDTO<CaratulaDto>();
        //    respuesta.IsSuccess = false;

        //    try
        //    {
        //        var responseDTO = _balanceBusiness.GetById(id);

        //        if (responseDTO.IsSuccess)
        //        {
        //            var balance = responseDTO.Result;
        //            balance.Caratula = _mapper.Map<Caratula>(caratuladto);
        //        }
        //        var caratuladto = balancersp.Result.Caratula;
        //        var caratula = 


        //        respuesta.IsSuccess = true;
        //        respuesta.Result = caratula;
        //        respuesta.Message = "caratula encontrada exitosamente";
        //    }
        //    catch (Exception ex)
        //    {

        //        respuesta.Message = ex.Message;

        //    }

        //    return respuesta;
        //}

        public ResponseDTO<Balance> Insert(CaratulaDto modelo)
        {
            ResponseDTO<Balance> respuesta = new ResponseDTO<Balance>();
            respuesta.IsSuccess = false;

            try
            {
                var balance = new Balance();


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

            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;


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







        //public CaratulaDto Insert(CaratulaDto modelo)
        //{
        //    try
        //    {
        //        var balance = _mapper.Map<Balance>(modelo);
        //        var rsp = _balances.InsertOneAsync(balance);

        //        if (rsp.Id != null) return _mapper.Map<BalanceDto>(rsp);
        //        else throw new NotImplementedException("No se pudo crear");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


    }
}
