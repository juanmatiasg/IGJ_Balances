using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Balances.Services.Implementation
{
    public class BalanceService : IBalanceService
    {
        private IMongoCollection<Balance> _balances;
        private readonly ILogger<BalanceService> _logger;


        public BalanceService(IMongoDbSettings _settings, ILogger<BalanceService> logger)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balances = database.GetCollection<Balance>(_settings.Collection);
            _logger = logger;

        }

        public bool DeleteArchivoBalance(string balanceId, string archivoId)
        {
            try
            {
                var balance = GetById(balanceId);

                var archivo = balance.Archivos.First(d => d.Id == archivoId);
                balance.Archivos.Remove(archivo);

                UpdateBalance(balance);
                _logger.LogInformation("BalanceService.DeleteArchivoBalance: correcto");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"BalanceService.DeleteArchivoBalance: \n {ex}");
                return false;
            }
        }

        public bool DeleteBalance(string id)
        {
            try
            {
                _balances.DeleteOneAsync(d => d.Id == id);
                _logger.LogInformation("BalanceService.DeleteBalance: correcto");
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"BalanceService.DeleteBalance: \n {ex}");
                return false;
            }

        }

        public List<Balance> GetAll(string correlativo)
        {
            return _balances.Find(p => p.Caratula.Entidad.Correlativo == correlativo).ToList();
        }

        public Balance GetById(string id)
        {
            var balance = _balances.Find(
                new BsonDocument { { "_id", new ObjectId(id) } }
               ).FirstOrDefaultAsync().Result;

            return balance;
        }

        public bool InsertBalance(Balance balance)
        {
            try
            {
                _balances.InsertOneAsync(balance);

                _logger.LogInformation("BalanceService.InsertBalance: correcto");
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogError($"BalanceService.InsertBalance: \n {ex}");
                return false;
            }

        }

        public void UpdateBalance(Balance balance)
        {
            try
            {
                _balances.ReplaceOneAsync(b => b.Id == balance.Id, balance);
                _logger.LogInformation("BalanceService.UpdateBalance: correcto");
            }
            catch (Exception ex)
            {

                _logger.LogError($"BalanceService.UpdateBalance: \n {ex}");
            };
        }

    }
}


