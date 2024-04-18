using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface ICaratulaClientService
    {
        Task<ResponseDTO<BusquedaEntidadResponse>> findEntities(string nroCorrelativo);

        Task<ResponseDTO<BalanceDto>> initTramite(CaratulaDto caratulaDto);

        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<string>> getSession();

        Task<ResponseDTO<string>> setSession(string id);

        Task<ResponseDTO<string>> loadCaratula(string id);


    }
}
