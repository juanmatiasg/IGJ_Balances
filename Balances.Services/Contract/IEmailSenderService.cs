using Balances.DTO;
using Dominio.Helpers;

namespace EmailSender
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailAsync(MailRequest request);
        MailRequest EmaiInicioTramite(BalanceDto balance);
        MailRequest EmailPresentacion(BalanceDto balance, string html);

    }
}
