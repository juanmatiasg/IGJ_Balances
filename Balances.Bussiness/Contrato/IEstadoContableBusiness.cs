using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IEstadoContableBusiness
    {
        ResponseDTO<BalanceDto> InsertEECC(EstadoContableDto modelo);

        ResponseDTO<BalanceDto> InsertRubro(RubroPatrimonioNetoDto modelo);

        ResponseDTO<BalanceDto> DeleteRubro(RubroPatrimonioNetoDto modelo);

    }
}
