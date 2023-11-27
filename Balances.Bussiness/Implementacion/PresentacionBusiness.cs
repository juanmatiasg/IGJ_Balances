﻿using Balances.Bussiness.Contrato;
using Balances.DTO;
using Balances.Services.Contract;
using Dominio.Helpers;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
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



        private readonly IPDFService _pdfService;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public PresentacionBusiness(IBalanceBusiness balanceBusiness,
                                    IEmailSenderService emailSenderService,
                                    IQRService qRService,
                                    IPresentacionService presentacionService,


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

        }

        public ResponseDTO<BalanceDto> PresentarTramite()
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            //busco balance
            var bal = _balanceBusiness.BalanceActual;

            //genero QR (PNG en Base 64) con el id  como enlace oculto
            var qr = _qRService.QRGenerator(bal.Id);

            //filtro busqueda autoridad y socio condicion firmante para llenar la plantilla
            var balPresentacion = _presentacionService.GetBalanceAutoridadySocioFirmante(bal);
            // lleno la plantilla con los datos del balance
            var plantillahtmlpdf = _presentacionService.PlantillaPresentacionHtml(balPresentacion, qr);

            //pdf

            var binariopdf = _pdfService.HtmlToPDF(plantillahtmlpdf, balPresentacion);


            var plantillahtml = _presentacionService.CrearMailPresentacion(balPresentacion, qr);

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
            }


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
            var Plantillahtml = _presentacionService.PlantillaPresentacionHtml(balPresentacionfiltro, qr);








            return Plantillahtml;
        }

        public MailRequest CrearEmaiInicioTramite(BalanceDto balance)
        {
            var mailRequest = new MailRequest()
            {

                To = balance.Caratula.Email,
                Subject = $"Presentación Digital de Balances - {balance.Caratula.Entidad.RazonSocial} ",
                Body = CrearBodyBuilder(balance).HtmlBody,
            };

            return mailRequest;
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