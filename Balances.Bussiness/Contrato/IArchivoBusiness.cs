using Balances.DTO;
using Microsoft.AspNetCore.Http;

namespace Balances.Bussiness.Contrato
{
    public interface IArchivoBusiness
    {
        ResponseDTO<BalanceDto> Delete(ArchivoDTO modelo);
        ResponseDTO<BalanceDto> Upload(IFormFileCollection files);
    }
}
