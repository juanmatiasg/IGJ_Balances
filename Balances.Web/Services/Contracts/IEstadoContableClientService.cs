using Balances.DTO;
using Balances.Model;

namespace Balances.Web.Services.Implementation
{
    public interface IEstadoContableClientService
    {
        Task<ResponseDTO<BalanceDto>> insertEECC(EstadoContableDto estadoContableDto);

        Task<ResponseDTO<BalanceDto>> insertRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto);

        Task<ResponseDTO<BalanceDto>> deleteRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto);

        Task<ResponseDTO<string>> getSession();

        Task<ResponseDTO<BalanceDto>> getBalance(string id);
    }
}
