using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IArchivoBusiness
    {
        ResponseDTO<BalanceDto> Delete(ArchivoDTO archivo);
        ResponseDTO<BalanceDto> UploadFilesDTO(List<ArchivoDTO> files);
    }
}
