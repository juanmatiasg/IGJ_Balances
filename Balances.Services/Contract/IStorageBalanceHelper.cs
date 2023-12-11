using Microsoft.AspNetCore.Http;
using System.Data;

namespace Balances.Web.Services.Implementation
{
    public interface IStorageBalanceHelper
    {
        public void SetBalanceId(string balanceId);
        public string GetBalanceId();

    }
}
