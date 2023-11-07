using Balances.DTO;

namespace Balances.WebAssembly.Services.Contract
{
    public interface IBusquedaCuilOrCorrelativoService
    {
         Task <ResponseDTO<BusquedaEntidadResponse>> GetByCuilOrCorrelativo(int id);
    }
}
