using Balances.DTO;
using Dominio.Helpers;

namespace Balances.Bussiness.Contrato
{
    public interface IPresentacionBusiness
    {
        ResponseDTO<BalanceDto> PresentarTramite(string sesionId);

        MailRequest CrearEmailPresentacion(BalanceDto balance, string html, byte[] pdfPresentacion, string qr);

        String FormatPresentacionHTML(string sesionId);
    }
}
