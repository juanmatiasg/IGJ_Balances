using Balances.Model;

namespace Balances.Services.Contract
{
    public interface IBalanceService
    {
        bool InsertBalance(Balance balance);
        bool DeleteBalance(string id);
        void UpdateBalance(Balance balance);
        List<Balance> GetAll(string correlativo);
        Balance GetById(string id);
        bool DeleteArchivoBalance(string balanceId, string id);

    }
}
