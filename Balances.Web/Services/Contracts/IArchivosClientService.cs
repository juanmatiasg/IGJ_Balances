using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IArchivosClientService
    {
        Task<ResponseDTO<BalanceDto>> UploadArchivo(List<ArchivoDTO> files);

        Task<ResponseDTO<BalanceDto>> deleteArchivo(ArchivoDTO archivo);

    }
}
