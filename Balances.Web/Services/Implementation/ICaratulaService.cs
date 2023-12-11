using Balances.DTO;
using Balances.Model;

namespace Balances.Web.Services.Implementation
{
    public interface ICaratulaService
    {
        Task<ResponseDTO<BusquedaEntidadResponse>> findEntities(string nroCorrelativo);

        Task<ResponseDTO<BalanceDto>> initTramite(CaratulaDto caratulaDto);

        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<string>> getSession();


    }
}
