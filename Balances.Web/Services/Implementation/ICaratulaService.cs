using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface ICaratulaService
    {
        Task<ResponseDTO<BusquedaEntidadResponse>> findEntities(string nroCorrelativo);

    }
}
