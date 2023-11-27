using Balances.DTO;
using Dominio.Helpers;
using MimeKit;

namespace EmailSender
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailAsync(MailRequest request);
        Task<bool> SendEmailAsync(MimeMessage request);
        MailRequest EmaiInicioTramite(BalanceDto balance);
        MailRequest EmailPresentacion(BalanceDto balance, string html, byte[] pdfPresentacion, string qr);

    }
}
