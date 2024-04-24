using Balances.DTO;

namespace Balances.Web.Services.Contracts
{
    public interface ISessionClientService
    {
        Task<ResponseDTO<string>> getNewSession();
        //Task<ResponseDTO<string>> getBalanceId(string sesionId);
        Task<string> getBalanceId(string sesionId);
        Task<ResponseDTO<bool>> setBalanceId(string sessionId, string balanceId);

        Task<string> GetSessionBalanceId();
        //Task<ResponseDTO<string>> setSession();


    }
}
