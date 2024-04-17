using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface ILibrosClientService
    {
        Task<ResponseDTO<BalanceDto>> insertLibros(LibrosDto libro);
        
        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<string>> getSession();
    }
}
