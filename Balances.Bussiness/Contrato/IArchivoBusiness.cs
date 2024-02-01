using Balances.DTO;

namespace Balances.Bussiness.Contrato
{
    public interface IArchivoBusiness
    {
        ResponseDTO<BalanceDto> Delete(ArchivoDTO modelo);
        ResponseDTO<BalanceDto> Upload(UploadFilesDTO files);
    }
}
