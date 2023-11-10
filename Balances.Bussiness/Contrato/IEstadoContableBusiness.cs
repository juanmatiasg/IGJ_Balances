using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IEstadoContableBusiness
    {
        ResponseDTO<BalanceDto> Insert(EstadoContableDto modelo);

        ResponseDTO<BalanceDto> Insert(RubroPatrimonioNetoDto modelo);

        ResponseDTO<BalanceDto> Delete(RubroPatrimonioNetoDto modelo);





    }
}
