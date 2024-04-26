using Balances.DTO;
using Balances.Services.Contract;
using Dominio.Helpers;
using EmailSender;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;

namespace Balances.Services.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ISessionService _sessionService;
        private readonly IBalanceService _balanceService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmailSenderService> _logger;

        public EmailSenderService(IOptions<SmtpSettings> smtpSettings,
                                  ISessionService sessionService,
                                  IBalanceService balanceService,
                                  IWebHostEnvironment webHostEnvironment,
                                   ILogger<EmailSenderService> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _sessionService = sessionService;
            _balanceService = balanceService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        public MailRequest EmailPresentacion(BalanceDto balance, string html, byte[] pdfPresentacion, string qr)
        {
            var mailRequest = new MailRequest()
            {
                To = balance.Caratula.Email,
                Subject = $"Presentacion Generada - {balance.Caratula.Entidad.RazonSocial} ",
                Body = html,

            };

            mailRequest.Adjuntos.Add(new MailRequest.ArchivoAdjunto()
            {
                Nombre = balance.Id + ".pdf",
                TipoArchivo = "aplication/pdf",
                Binario = pdfPresentacion
            });

            mailRequest.Adjuntos.Add(new MailRequest.ArchivoAdjunto()
            {
                Nombre = "qr.png",
                TipoArchivo = "image/png",
                Binario = System.Convert.FromBase64String(qr)
            });
            return mailRequest;
        }


        public MailRequest EmaiInicioTramite(BalanceDto balance)
        {
            var mailRequest = new MailRequest()
            {

                To = balance.Caratula.Email,
                Subject = $"Presentación Digital de Balances - {balance.Caratula.Entidad.RazonSocial} ",
                Body = ConstructorBody(balance).HtmlBody,
            };

            return mailRequest;
        }

        public async Task<bool> SendEmailAsync(MimeMessage message)
        {
            var emailSerializado = JsonConvert.SerializeObject(message.To.ToString());
            try
            {
                /*
                var balanceId = _sessionService.GetBalanceId();
                var balance = _balanceService.GetById(balanceId);
                */
                //var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                //message.To.Add(new MailboxAddress("", request.To));
                /*message.Subject = request.Subject; //$"Presentación Digital de Balances - {balance.Caratula.Entidad.RazonSocial} "; /*request.Subject*/
                //message.Body = new TextPart("html") { Text = request.Body };
                // message.Body = new TextPart("html") { Text = request.Body };*/


                /*

                var multipart = new Multipart("mixed");
                //si tienearchivo adjunto
                if (request.Adjuntos != null)
                {
                    foreach (var adjunto in request.Adjuntos)
                    {
                        var attachment = new MimePart(adjunto.TipoArchivo)
                        {
                            Content = new MimeContent(new MemoryStream(adjunto.Binario)),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = adjunto.Nombre
                        };

                        multipart.Add(attachment);
                    }


                    multipart.Add(message.Body);

                    message.Body = multipart;
                }
                */
                using (var client = new SmtpClient())
                {

                    await client.ConnectAsync(_smtpSettings.Server, Convert.ToInt16(_smtpSettings.Port), SecureSocketOptions.None);

                    //await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);

                    _logger.LogInformation($"Email enviado correctamente a: \n {emailSerializado}");
                    return true;
                }       
            }
            catch (Exception ex)
            {
                _logger.LogError($"EmailSenderService.SendEmailAsync \n To: {emailSerializado} ");
                return false;
            }
        }

        public BodyBuilder ConstructorBody(BalanceDto balance)
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

        public Task<bool> SendEmailAsync(MailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}