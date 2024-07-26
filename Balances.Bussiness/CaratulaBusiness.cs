using AutoMapper;
using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Utils;
using Newtonsoft.Json;

namespace Balances.Bussiness
{
    public class CaratulaBusiness : ICaratulaBusiness
    {


        protected IMapper _mapper;
        private ISessionService _sessionService;
        private readonly IBalanceBusiness _balanceBusiness;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CaratulaBusiness> _logger;


        public CaratulaBusiness(IMapper mapper,
                                ISessionService sessionService,
                                IBalanceBusiness balanceBusiness,
                                IEmailSenderService emailSenderService,
                                IWebHostEnvironment webHostEnvironment,
                                ILogger<CaratulaBusiness> logger)
        {

            _mapper = mapper;
            _sessionService = sessionService;
            _balanceBusiness = balanceBusiness;
            _emailSenderService = emailSenderService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        public ResponseDTO<Balance> Insert(CaratulaDto modelo)
        {
            ResponseDTO<Balance> respuesta = new ResponseDTO<Balance>();
            respuesta.IsSuccess = false;
            var caratulaSerializada = JsonConvert.SerializeObject(modelo);
            try
            {

                var balance = new Balance();

                balance.Caratula = MapearCaratula(modelo);

                var rsp = _balanceBusiness.Insert(balance);

                var balanceDto = _mapper.Map<BalanceDto>(balance);

                //guardo el balance en la sesion
                _sessionService.SetBalance(modelo.SesionId, rsp.Result.Id);


                var plantillahtml = CrearPlantillaInicioTramite(balanceDto);

                var email = CrearEmaiInicioTramite(balanceDto, plantillahtml);
                //var EmailRequest = _emailSenderService.EmaiInicioTramite(balanceDto);
                _emailSenderService.SendEmailAsync(email);

                // si inserto correctamente
                if (rsp != null)
                {
                    //_sessionService.SetSession(balance.Id);

                    respuesta.IsSuccess = true;
                    respuesta.Message = "Caratula creada correctamente";
                    respuesta.Result = balance;
                    _logger.LogInformation($"CaratulaBusiness.Insert --> {caratulaSerializada}");
                }



            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"CaratulaBusiness.Insert  \n {caratulaSerializada}  \n {ex.Message} ");


            }
            return respuesta;
        }


        public ResponseDTO<BalanceDto> Rectificar(BalanceDto balance)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var caratulaSerializada = JsonConvert.SerializeObject(balance.Caratula);

            try
            {

                var rst = _balanceBusiness.Update(balance);


                var plantillahtml = CrearPlantillaInicioTramite(balance);

                var email = CrearEmaiInicioTramite(balance, plantillahtml);
                //var EmailRequest = _emailSenderService.EmaiInicioTramite(balanceDto);
                _emailSenderService.SendEmailAsync(email);

                // si inserto correctamente
                if (rst != null)
                {
                    //_sessionService.SetSession(balance.Id);

                    respuesta.IsSuccess = true;
                    respuesta.Message = "Caratula creada correctamente";
                    respuesta.Result = balance;
                    _logger.LogInformation($"CaratulaBusiness.Insert --> {caratulaSerializada}");
                }



            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"CaratulaBusiness.Insert  \n {caratulaSerializada}  \n {ex.Message} ");


            }
            return respuesta;
        }


        public ResponseDTO<BalanceDto> Update(CaratulaDto modelo)
        {
            ResponseDTO<BalanceDto> respuesta = new ResponseDTO<BalanceDto>();
            respuesta.IsSuccess = false;
            var caratulaSerializada = JsonConvert.SerializeObject(modelo);
            try
            {

                var id = _sessionService.GetBalanceId(modelo.SesionId);

                var balanceDto = _balanceBusiness.GetById(id);

                var modeloDto = _mapper.Map<Caratula>(modelo);
                balanceDto.Result.Caratula = modeloDto;
                var rsp = _balanceBusiness.Update(balanceDto.Result);

                // si actualizo correctamente

                if (rsp != null)
                {


                    respuesta.IsSuccess = true;
                    respuesta.Message = "Caratula actualizada correctamente";
                    respuesta.Result = rsp.Result;
                    _logger.LogInformation($"CaratulaBusiness.Update --> {caratulaSerializada}");
                }

                var plantillahtml = CrearPlantillaInicioTramite(balanceDto.Result);

                var email = CrearEmaiInicioTramite(balanceDto.Result, plantillahtml);
                //var EmailRequest = _emailSenderService.EmaiInicioTramite(balanceDto);
                _emailSenderService.SendEmailAsync(email);






            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
                _logger.LogError($"CaratulaBusiness.Insert  \n {caratulaSerializada}  \n {ex.Message} ");


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

            var PlantillaHTML = System.IO.File.ReadAllText(Plantilla);
            return PlantillaHTML;
        }

        public MimeMessage CrearEmaiInicioTramite(BalanceDto balance, string html)
        {
            var mime = new MimeMessage();

            try
            {
                mime.Subject = $"Presentacion Generada - {balance.Caratula.Entidad.RazonSocial} ";
                //var  mime = new MimeMessage()
                // {
                //     //    To = balance.Caratula.Email,
                //     Subject = $"Presentacion Generada - {balance.Caratula.Entidad.RazonSocial} ",
                //     //  Body = html,

                // };

                mime.To.Add(new MailboxAddress("", balance.Caratula.Email));

                var builder = new BodyBuilder();
                var pathImage = _webHostEnvironment.ContentRootPath + "/Plantillas/Imagenes";

                /* A G R E G AM O S   I M A G E N E S   H E A D E R */
                var imgIGJ = builder.LinkedResources.Add("igj.png", System.IO.File.ReadAllBytes(pathImage + "/igj.png"));
                imgIGJ.ContentId = MimeUtils.GenerateMessageId();
                html = html.Replace("{{igjImage}}", imgIGJ.ContentId);

                var imgMIN = builder.LinkedResources.Add("ministerio.png", System.IO.File.ReadAllBytes(pathImage + "/ministerio.png"));
                imgMIN.ContentId = MimeUtils.GenerateMessageId();
                html = html.Replace("{{MinImage}}", imgMIN.ContentId);



                builder.HtmlBody = html;



                mime.Body = builder.ToMessageBody();

                _logger.LogInformation("se ha armado el mail correctamente");



            }
            catch (Exception ex)
            {
                _logger.LogError($"CaratulaBusiness.CrearMailInicioDelTramite \n {ex}");

            }
            return mime;

        }

        private Caratula MapearCaratula(CaratulaDto modelo)
        {
            var caratula = new Caratula()
            {
                FechaDeCierre = (DateTime)modelo.FechaDeCierre,
                Email = modelo.Email,
                Entidad = modelo.Entidad,
                FechaInicio = (DateTime)modelo.FechaInicio,
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





    }
}