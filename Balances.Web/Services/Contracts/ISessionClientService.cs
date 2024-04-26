using Balances.DTO;

namespace Balances.Web.Services.Contracts
{
    public interface ISessionClientService
    {
        Task<ResponseDTO<string>> getNewSession();

        Task<string> getBalanceId(string sesionId);
        //Task<ResponseDTO<bool>> setBalanceId(string sessionId, string balanceId);
        Task<bool> setBalanceId(string sessionId, string balanceId);
        Task<string> GetSessionBalanceId();



    }
}
