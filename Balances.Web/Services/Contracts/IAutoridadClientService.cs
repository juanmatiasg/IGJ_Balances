using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IAutoridadClientService
    {
        Task<ResponseDTO<BalanceDto>> insertAutoridad(AutoridadDto autoridad);

        Task<ResponseDTO<BalanceDto>> deleteAutoridad(AutoridadDto autoridad);

        Task<ResponseDTO<BalanceDto>> updateAutoridad(AutoridadDto autoridad);

    }
}
