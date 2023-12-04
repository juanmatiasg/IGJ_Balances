using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Balances.Services.Implementation
{
    public class BalanceService : IBalanceService
    {
        private IMongoCollection<Balance> _balances;


        public BalanceService(IMongoDbSettings _settings)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balances = database.GetCollection<Balance>(_settings.Collection);
        }

        public bool DeleteArchivoBalance(string balanceId, string archivoId)
        {
            try
            {
                var balance = GetById(balanceId);

                var archivo = balance.Archivos.First(d => d.Id == archivoId);
                balance.Archivos.Remove(archivo);

                UpdateBalance(balanceId, balance);

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public void DeleteBalance(string id)
        {
            _balances.DeleteOneAsync(d => d.Id == id);
        }

        public List<Balance> GetAll()
        {
            return _balances.Find(p => true).ToList();
        }

        public Balance GetById(string id)
        {
            var balance = _balances.Find(
                new BsonDocument { { "_id", new ObjectId(id) } }
               ).FirstOrDefaultAsync().Result;

            return balance;
        }

        public void InsertBalance(Balance balance)
        {
            _balances.InsertOneAsync(balance);
        }

        public void UpdateBalance(string id, Balance balance)
        {
            _balances.ReplaceOneAsync(b => b.Id == id, balance);
        }
    }

}
