using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IAutoridadesBusiness
    {
        ResponseDTO<BalanceDto> Insert(AutoridadDto modelo);
        ResponseDTO<BalanceDto> Delete(AutoridadDto modelo);

        ResponseDTO<BalanceDto> Update(AutoridadDto modelo);

    }
}
