using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Model;
using Balances.Services.Contract;
using Dominio.Helpers;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Utils;
using Newtonsoft.Json;

namespace Balances.Bussiness.Implementacion
{
    public class PresentacionBusiness : IPresentacionBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;

        private readonly ISessionService _sessionService;

        private readonly IEmailSenderService _emailSenderService;

        private readonly IQRService _qRService;

        private readonly IPresentacionService _presentacionService;

        private readonly ILogger<IPresentacionBusiness> _logger;

        private readonly IPDFService _pdfService;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public PresentacionBusiness(IBalanceBusiness balanceBusiness,
                                    IEmailSenderService emailSenderService,
                                    ISessionService sessionService,
                                    IQRService qRService,
                                    IPresentacionService presentacionService,
                                    ILogger<IPresentacionBusiness> logger,
                                    IPDFService pdfService,
                                    IWebHostEnvironment webHostEnvironment
                                    )
        {
            _balanceBusiness = balanceBusiness;
            _sessionService = sessionService;
            _emailSenderService = emailSenderService;
            _qRService = qRService;
            _presentacionService = presentacionService;
            _pdfService = pdfService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;


        }

        public ResponseDTO<BalanceDto> PresentarTramite(string sesionId)
        {

            _logger.LogInformation("Metodo presentar tramite invocado");

            var respuesta = new ResponseDTO<BalanceDto>();
            //busco balance
            var id = _sessionService.GetBalanceId(sesionId);

            var bal = _balanceBusiness.GetById(id);

            BalanceResumen resumen = BalanceMapper.MapToResumen(bal.Result);
            var resumenBalSerializado = JsonConvert.SerializeObject(resumen);
            string hash = HashHelper.CalculateHash(resumenBalSerializado);
            bal.Result.HASH = hash;

            //genero QR (PNG en Base 64) con el HASH  como enlace oculto
            var qr = _qRService.QRGenerator(bal.Result.HASH);



            // lleno las plantillas con los datos del balance
            var plantillapdf = _presentacionService.CrearPlantillaPresentacionPdf(bal.Result, qr);
            var plantillahtml = _presentacionService.CrearPlantillaPresentacionEmail(bal.Result, qr);


            //pdf

            var binariopdf = _pdfService.HtmlToPDF(plantillapdf, bal.Result);


            //agrego la presentacion al balance
            if (bal.Result.Presentacion == null)
            {
                bal.Result.Presentacion = new Presentacion();
            }

            bal.Result.Presentacion.Fecha = DateTime.Now;
            bal.Result.Presentacion.BinarioPdf = binariopdf;
            _balanceBusiness.Update(bal.Result);



            //File.WriteAllBytes("c:/prueba.pdf", pdf);
            // paso como parametro el balance y la plantilla para armar el emailRequest 
            var EmailRequest = CrearEmailPresentacion(bal.Result, plantillahtml, binariopdf, qr);

            var presentacionSerializada = JsonConvert.SerializeObject(bal);
            try
            {
                //Envio el email con los datos del EmailRequest
                _emailSenderService.SendEmailAsync(EmailRequest);

                respuesta.Message = "Presentacion generada y enviada correctamente";
                respuesta.Result = bal.Result;
                respuesta.IsSuccess = true;
                _logger.LogInformation($" PresentacionBusiness.PresentarTramite: ---> {presentacionSerializada}");

            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError($"PresentacionBusiness.PresentarTramite: \n {ex}");
            }

            //actualizo la base con los datos de la presentacion

            return respuesta;
        }

        public string FormatPresentacionHTML(string sesionId)
        {

            //busco balance
            var id = _sessionService.GetBalanceId(sesionId);

            var bal = _balanceBusiness.GetById(id);

            //genero QR
            var qr = _qRService.QRGenerator(bal.Result.Id);
            // lleno la plantilla con los datos del balance
            //var balPresentacionfiltro = _presentacionService.GetBalanceAutoridadySocioFirmante(bal);
            var Plantillahtml = _presentacionService.CrearPlantillaPresentacionPdf(bal.Result, qr);

            return Plantillahtml;
        }



        public MimeMessage CrearEmailPresentacion(BalanceDto balance, string html, byte[] binariopdf, string qr)
        {
            var mime = new MimeMessage()
            {
                //    To = balance.Caratula.Email,
                Subject = $"Manifiesto - {balance.Caratula.Entidad.RazonSocial} ",
                //  Body = html,

            };

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

            /* A G R E G A M O S   Q R */
            var imgQr = builder.LinkedResources.Add("qr.png", Convert.FromBase64String(qr));
            imgQr.ContentId = MimeUtils.GenerateMessageId();
            html = html.Replace("{{QR}}", imgQr.ContentId);

            builder.HtmlBody = html;

            builder.Attachments.Add(balance.Id + ".pdf", binariopdf);

            mime.Body = builder.ToMessageBody();

            return mime;
        }

        public BodyBuilder CrearBodyBuilder(BalanceDto balance)
        {

            var path = _webHostEnvironment.ContentRootPath + "/Plantillas";
            var Plantilla = path + "/PlantillaEmail.html";
            var PlantillaHTML = System.IO.File.ReadAllText(Plantilla);

            PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
            PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
            PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
            PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
            PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
            PlantillaHTML = PlantillaHTML.Replace("{{BalanceId}}", balance.Id);

            var bodyBuilder = new BodyBuilder();


            bodyBuilder.HtmlBody = PlantillaHTML;


            return bodyBuilder;
        }

        MailRequest IPresentacionBusiness.CrearEmailPresentacion(BalanceDto balance, string html, byte[] pdfPresentacion, string qr)
        {
            throw new NotImplementedException();
        }

    }
}
