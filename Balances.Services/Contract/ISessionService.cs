namespace Balances.Services.Contract
{
    public interface ISessionService
    {
        string BalanceId { get; set; }

        void SetBalanceId(string balanceId);
        string GetBalanceId();
    }
}
