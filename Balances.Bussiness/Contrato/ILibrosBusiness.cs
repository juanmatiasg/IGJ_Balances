using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface ILibrosBusiness
    {
        ResponseDTO<BalanceDto> Insert(LibrosDto modelo);

        ResponseDTO<BalanceDto> Delete(LibroDto modelo);
    }
}
