using Balances.DTO;

namespace Balances.Web.Services.Contracts
{
    public interface IBaseSessionClientService
    {
        Task<ResponseDTO<string>> getSession();
        Task<ResponseDTO<string>> setSession(string balanceid);
    }
}
