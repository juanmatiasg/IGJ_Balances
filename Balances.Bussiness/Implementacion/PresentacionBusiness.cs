using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Dominio.Helpers;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Utils;


namespace Balances.Bussiness.Implementacion
{
    public class PresentacionBusiness : IPresentacionBusiness
    {
        private readonly IBalanceBusiness _balanceBusiness;

        private readonly IEmailSenderService _emailSenderService;

        private readonly IQRService _qRService;

        private readonly IPresentacionService _presentacionService;

        private readonly ILogger<IPresentacionBusiness> _logger;

        private readonly IPDFService _pdfService;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public PresentacionBusiness(IBalanceBusiness balanceBusiness,
                                    IEmailSenderService emailSenderService,
                                    IQRService qRService,
                                    IPresentacionService presentacionService,
                                    ILogger<IPresentacionBusiness> logger,
                                    IPDFService pdfService,
                                    IWebHostEnvironment webHostEnvironment
                                    )
        {
            _balanceBusiness = balanceBusiness;
            _emailSenderService = emailSenderService;
            _qRService = qRService;
            _presentacionService = presentacionService;
            _pdfService = pdfService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;

        }

        public ResponseDTO<BalanceDto> PresentarTramite()
        {

            _logger.LogInformation("Metodo presentar tramite invocado");

            var respuesta = new ResponseDTO<BalanceDto>();
            //busco balance
            var bal = _balanceBusiness.BalanceActual;

            //genero QR (PNG en Base 64) con el id  como enlace oculto
            var qr = _qRService.QRGenerator(bal.Id);

            //filtro busqueda autoridad y socio condicion firmante para llenar la plantilla
            var balPresentacion = _presentacionService.GetBalanceAutoridadySocioFirmante(bal);
            // lleno la plantilla con los datos del balance
            var plantillapdf = _presentacionService.CrearPlantillaPresentacionPdf(balPresentacion, qr);

            //pdf

            var binariopdf = _pdfService.HtmlToPDF(plantillapdf, balPresentacion);

            //agrego la presentacion al balance
            bal.Presentacion.Fecha = DateTime.Now;
            bal.Presentacion.PdfBytes = binariopdf;

            var plantillahtml = _presentacionService.CrearPlantillaPresentacionEmail(balPresentacion, qr);

            //File.WriteAllBytes("c:/prueba.pdf", pdf);
            // paso como parametro el balance y la plantilla para armar el emailRequest 
            var EmailRequest = CrearEmailPresentacion(bal, plantillahtml, binariopdf, qr);

            try
            {
                //Envio el email con los datos del EmailRequest
                _emailSenderService.SendEmailAsync(EmailRequest);

                respuesta.Message = "Presentacion generada y enviada correctamente";
                respuesta.Result = bal;
                respuesta.IsSuccess = true;

            }
            catch (Exception ex)
            {

                respuesta.Message = ex.Message;
                _logger.LogError("PresentarTramite: SendEmailAsync", ex);
            }

            //actualizo la base con los datos de la presentacion
            _balanceBusiness.Update(bal);

            return respuesta;
        }

        public string FormatPresentacionHTML()
        {

            //busco balance
            var bal = _balanceBusiness.BalanceActual;

            //genero QR
            var qr = _qRService.QRGenerator(bal.Id);
            // lleno la plantilla con los datos del balance
            var balPresentacionfiltro = _presentacionService.GetBalanceAutoridadySocioFirmante(bal);
            var Plantillahtml = _presentacionService.CrearPlantillaPresentacionEmail(balPresentacionfiltro, qr);








            return Plantillahtml;
        }



        public MimeMessage CrearEmailPresentacion(BalanceDto balance, string html, byte[] binariopdf, string qr)
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
            var PlantillaHTML = File.ReadAllText(Plantilla);

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

        //public string PlantillaPresentacionHtml(BalanceDto balance, string qr)
        //{
        //    var path = _webHostEnvironment.ContentRootPath + "/Plantillas";
        //    var Plantilla = path + "/PlantillaPresentacionBalance.html";

        //    ////var Plantilla = path + "/PlantillaPresentacion.html";
        //    //var Plantilla = path + ROUTEHTML;

        //    var PlantillaHTML = File.ReadAllText(Plantilla);


        //    PlantillaHTML = PlantillaHTML.Replace("{{RazonSocial}}", balance.Caratula.Entidad.RazonSocial);
        //    PlantillaHTML = PlantillaHTML.Replace("{{TipoEntidad}}", balance.Caratula.Entidad.TipoEntidad);
        //    PlantillaHTML = PlantillaHTML.Replace("{{NroCorrelativo}}", balance.Caratula.Entidad.Correlativo);
        //    PlantillaHTML = PlantillaHTML.Replace("{{FechaEstado}}", balance.Caratula.FechaDeCierre.ToShortDateString());
        //    PlantillaHTML = PlantillaHTML.Replace("{{Domicilio}}", balance.Caratula.Entidad.Domicilio);
        //    PlantillaHTML = PlantillaHTML.Replace("{{QR}}", qr);


        //    return PlantillaHTML;
        //}

        //public string QRGenerator(string id)
        //{
        //    ;
        //    //CREAR QR CON DATA
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrData = QRCodeGenerator.GenerateQrCode(id, QRCodeGenerator.ECCLevel.Q);

        //    //VISUALIZACION DEL QR
        //    PngByteQRCode qrCode = new PngByteQRCode(qrData);
        //    byte[] qrCodeImage = qrCode.GetGraphic(5);

        //    //VISUALIZAR EN BASE  64
        //    string model = Convert.ToBase64String(qrCodeImage);
        //    return model;

        //}
    }
}
