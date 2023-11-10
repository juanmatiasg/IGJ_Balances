using Balances.DTO;
using Balances.Model;

namespace Balances.Services.Contract
{
    public interface ILibrosDigitalesService
    {
        void ActualizarLibrosDigitales(LibrosDigitalesDto librosDigitales);
        LibrosDigitalesDto Get(string balanceId);
        List<LibroDigital> GetNewList();
    }
}
