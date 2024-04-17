using Balances.DTO;
using Balances.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Balances.Web.Services.Implementation
{
    public interface IArchivosClientService
    {

        //Task<ResponseDTO<BalanceDto>> uploadArchivo(IReadOnlyList<IBrowserFile> files, string categoria);
        Task<ResponseDTO<BalanceDto>> uploadArchivo(List<FileDTO> files);

        Task<ResponseDTO<BalanceDto>> deleteArchivo(FileDTO archivo);

        Task<ResponseDTO<BalanceDto>> getBalance(string id);

        Task<ResponseDTO<string>> getSession();

    }
}
