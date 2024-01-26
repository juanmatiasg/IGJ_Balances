using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IBalanceBusiness
    {
        BalanceDto BalanceActual { get; }

        ResponseDTO<BalanceDto> Insert(BalanceDto modelo);

        ResponseDTO<BalanceDto> Update(BalanceDto modelo);

        ResponseDTO<bool> Delete(string id);

        ResponseDTO<BalanceDto> GetById(string id);

        ResponseDTO<IEnumerable<BalanceDto>> List();


    }
}
