using Dominio.Helpers;
using EmailSender;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace Balances.Services.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSenderService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest request)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", request.Email));
                message.Subject = "Inicio de Presentación Digital de Balances - @nombreEntidad"; /*request.Subject*/
                //message.Body = new TextPart("html") { Text = request.Body };
                message.Body = ConstructorBody().ToMessageBody();


                using (var client = new SmtpClient())
                {

                    await client.ConnectAsync(_smtpSettings.Server, Convert.ToInt16(_smtpSettings.Port), SecureSocketOptions.None);

                    //await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BodyBuilder ConstructorBody()
        {
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = @"
                        <div style='background-color:black;color:white;padding:10px;'>
                 
                            <h1 style='font-family: 'Sigmar One', cursive;'>Inicio de Presentación Digital de Balances - @nombreEntidad</h1>
                            <h2>Datos Presentacion</h2>
                            <p>Fecha De Inico: <b><i> </i> @fechainicio </b></p>
                            <p>Entidad <b><i> @nombreEntidad </i> </b></p>
                            <p>Cierre de Balance: <b><i> @fechacierre </i> </b></p>
                            <p>Para continuar con la carga de su balance, haga clic en el siguiente enlace:</p>
                            <p style='color:1678E7;'>https://balancesdesa.justicia.ar/?key=64e62685c54fd008c507c8c0 </p>
                            <p>Nuestro canal de consulta:infoigj@jus.gob.ar </p>
                            <p style='text-align:right;color:#1678E7;font-size:18px;' ><b><i>Inspeccion General de Justicia</i></b> </p>
                        
                        </div>
            ";

            return bodyBuilder;
        }
    }
}