using Balances.DTO;
using Balances.Model;
using Microsoft.AspNetCore.Http;

namespace Balances.Bussiness.Contrato
{
    public interface IArchivoBusiness
    {
        ResponseDTO<BalanceDto> Delete(Archivo archivo);
        ResponseDTO<BalanceDto> UploadFilesDTO(UploadFilesDTO ufDto);
    }
}
