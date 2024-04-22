using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IArchivosClientService
    {

        Task<ResponseDTO<BalanceDto>> uploadArchivo(List<FileDTO> files);

        Task<ResponseDTO<BalanceDto>> deleteArchivo(FileDTO archivo);


    }
}
