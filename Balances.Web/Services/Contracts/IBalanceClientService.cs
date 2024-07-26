using Balances.DTO;

namespace Balances.Web.Services.Contracts
{
    public interface IBalanceClientService
    {
        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<List<BalanceDto>>> listBalances(string correlativo);
    }
}
