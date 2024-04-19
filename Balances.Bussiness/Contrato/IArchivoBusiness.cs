using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IArchivoBusiness
    {
        ResponseDTO<BalanceDto> Delete(FileDTO archivo);
        ResponseDTO<BalanceDto> UploadFilesDTO(List<FileDTO> files);
    }
}
