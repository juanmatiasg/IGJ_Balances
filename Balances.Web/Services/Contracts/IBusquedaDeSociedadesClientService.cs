using Balances.DTO;

namespace Balances.Web.Services.Contracts
{
    public interface IBusquedaDeSociedadesClientService
    {
        Task<ResponseDTO<BusquedaEntidadResponse>> findSociedad(string nroCorrelativo);
    }
}
