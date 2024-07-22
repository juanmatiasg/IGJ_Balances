using Balances.DTO;
using Balances.Model;

namespace Balances.Bussiness.Contrato
{
    public interface ICaratulaBusiness
    {
        ResponseDTO<Balance> Insert(CaratulaDto modelo);

        ResponseDTO<BalanceDto> Update(CaratulaDto modelo);

        ResponseDTO<BalanceDto> Rectificar(BalanceDto balance);
    }
}