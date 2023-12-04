using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IContadorService
    {
        Task<ResponseDTO<BalanceDto>> postContador(ContadorDto contador);


        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<string>> setSession(string idBalance);

        Task<ResponseDTO<string>> getSession();



    }
}
