using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IPresentacionClientService
    {
        Task<ResponseDTO<BalanceDto>> generarPresentacion(string sesionId);
        Task<string> GenerarPresentacionEnHtml(string sesionId);
    }
}
