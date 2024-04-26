namespace Balances.Services.Contract
{
    public interface ISessionService
    {

        bool SetBalance(string sessionId, string balanceId);
        string GetNewSesion();
        string GetBalanceId(string sessionId);
        string GetSessionBalanceId();

    }


}
