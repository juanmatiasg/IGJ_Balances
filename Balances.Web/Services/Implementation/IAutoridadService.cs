using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface  IAutoridadService
    {
        Task<ResponseDTO<BalanceDto>> insertAutoridad(AutoridadDto autoridad);

        Task<ResponseDTO<BalanceDto>> deleteAutoridad(AutoridadDto autoridad);

     

    }
}
