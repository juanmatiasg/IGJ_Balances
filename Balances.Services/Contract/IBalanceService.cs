using Balances.Model;

namespace Balances.Services.Contract
{
    public interface IBalanceService
    {
        void InsertBalance(Balance balance);
        void DeleteBalance(string id);
        void UpdateBalance(string id, Balance balance);
        List<Balance> GetAll();
        Balance GetById(string id);
        bool DeleteArchivoBalance(string balanceId, string id);

    }
}
