using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IPresentacionService
    {
        Task<ResponseDTO<BalanceDto>> generarPresentacion();
    }
}
