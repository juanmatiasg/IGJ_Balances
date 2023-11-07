using Balances.Model;
using System.Linq.Expressions;

namespace Balances.Repository.Contract
{
    public interface IMongoRepository
    {
        void CreateBalance(Balance balance);
        Balance GetBalance(string id);
        List<Balance> GetAll();
        void UpdateBalance(string id, Balance balance);
        void DeleteBalance(string id);
    }
}