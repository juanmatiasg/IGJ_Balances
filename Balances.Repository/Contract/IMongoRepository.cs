using Balances.Model;

namespace Balances.Repository.Contract
{
    public interface IMongoRepository
    {
        void CreateBalance(Balance balance);
        Balance GetBalance(string id);
        List<Balance> GetAll();
        void UpdateBalance(string id, Balance balance);
        void DeleteBalance(string id);

        Balance Create(Balance balance);
        Balance GetById(string id);
        //List<Balance> GetAll();
        bool Update(string id, Balance balance);
        bool Delete(string id);
    }
}