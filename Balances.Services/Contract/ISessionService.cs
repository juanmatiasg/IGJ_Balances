using System.Collections.Generic;

namespace Balances.Services.Contract
{
    public interface ISessionService
    {

        void SetSession(string balanceId);
        Dictionary<string, string> GetSession();
        string GetSessionBalanceId();
        string GetSessionToken();
    }


}
