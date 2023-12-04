using Balances.DTO;
using Balances.Model;

namespace Balances.Web.Services.Implementation
{
    public interface ICaratulaService
    {
        Task<ResponseDTO<BusquedaEntidadResponse>> findEntities(string nroCorrelativo);

        Task<ResponseDTO<BalanceDto>> initTramite(string email, DateTime fechaInicio, DateTime fechaDeCierre, string razonSocial, string tipoEntidad, string domicilio, bool sedeSocialInscripta, string nroCorrelativo);

        Task<ResponseDTO<BalanceDto>> getBalance(string id);

  

    }
}
