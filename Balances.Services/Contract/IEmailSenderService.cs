using Dominio.Helpers;

namespace EmailSender
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
