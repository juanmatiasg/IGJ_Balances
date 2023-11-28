using Balances.DTO;
using Balances.Model;

namespace Balances.Services.Contract
{
    public interface ILibrosDigitalesService
    {
        void ActualizarLibrosDigitales(LibrosDto librosDigitales);
        LibrosDto Get(string balanceId);
        List<Libro> GetNewList();
    }
}
