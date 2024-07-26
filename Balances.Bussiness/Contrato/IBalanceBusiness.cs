using Balances.DTO;
using Balances.Model;

namespace Balances.Bussiness.Contrato
{
    public interface IBalanceBusiness
    {
        BalanceDto BalanceActual { get; }

        ResponseDTO<Balance> Insert(Balance modelo);

        ResponseDTO<BalanceDto> Update(BalanceDto modelo);

        ResponseDTO<bool> Delete(string id);

        ResponseDTO<BalanceDto> GetById(string id);

        ResponseDTO<IEnumerable<BalanceDto>> List(string correlativo);


    }
}
