using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IAutoridadesBusiness
    {
        ResponseDTO<BalanceDto> Insert(AutoridadDto modelo);

        bool Update(AutoridadesLegalesDto modelo);

        ResponseDTO<BalanceDto> Delete(AutoridadDto modelo);



        //AutoridadesLegalesDto List();
    }
}
