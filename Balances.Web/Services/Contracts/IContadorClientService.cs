using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IContadorClientService
    {
        Task<ResponseDTO<BalanceDto>> postContador(ContadorDto contador);

    }
}
