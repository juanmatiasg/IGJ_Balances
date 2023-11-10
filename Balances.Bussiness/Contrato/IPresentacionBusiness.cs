using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IPresentacionBusiness
    {
        ResponseDTO<BalanceDto> PresentarTramite();
        String FormatPresentacionHTML();
    }
}
