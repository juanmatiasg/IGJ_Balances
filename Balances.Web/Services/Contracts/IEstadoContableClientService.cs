using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IEstadoContableClientService
    {
        Task<ResponseDTO<BalanceDto>> insertEECC(EstadoContableDto estadoContableDto);

        Task<ResponseDTO<BalanceDto>> insertRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto);

        Task<ResponseDTO<BalanceDto>> deleteRubro(RubroPatrimonioNetoDto rubroPatrimonioNetoDto);


    }
}
