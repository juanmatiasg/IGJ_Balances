using Balances.DTO;

namespace Balances.Web.Services.Implementation
{
    public interface IArchivosClientService
    {

        Task<ResponseDTO<BalanceDto>> uploadArchivo(List<ArchivoDTO> files);

        Task<ResponseDTO<BalanceDto>> deleteArchivo(ArchivoDTO archivo);


    }
}
