using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IContadorBusiness
    {
        ResponseDTO<BalanceDto> Insert(ContadorDto modelo);

        bool Update(ContadorDto modelo);

        bool Delete(ContadorDto modelo);

        ContadorDto GetById(string id);

        IEnumerable<ContadorDto> List();
    }
}
