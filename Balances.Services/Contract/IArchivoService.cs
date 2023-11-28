using Microsoft.AspNetCore.Http;

namespace Balances.Services.Contract
{
    public interface IArchivoService
    {
        void Delete(string balanceId, string id);
        bool Upload(string balanceId, IFormFileCollection files);
    }
}
