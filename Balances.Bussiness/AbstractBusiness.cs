using AutoMapper;
using Balances.Model;
using Balances.Repository.Contract;
using Balances.Services.Contract;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Balances.Bussiness
{
    public class AbstractBusiness
    {
        protected IMongoCollection<Balance> _balances;
        protected IMapper _mapper;
        protected readonly ISessionService _sessionService;

        public AbstractBusiness(IMongoDbSettings _settings, IMapper mapper, ISessionService sessionService)
        {
            var cliente = new MongoClient(_settings.Server);
            var database = cliente.GetDatabase(_settings.Database);
            _balances = database.GetCollection<Balance>(_settings.Collection);
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public Balance GetBalance(string sesionId)
        {

            var id = _sessionService.GetBalanceId(sesionId);


            //var resultadoDto = _balanceBusiness.GetById(id.Values.ToString());
            //var balanceId = _sessionService.GetSession();


            var balance = _balances.Find(
                new BsonDocument { { "_id", new ObjectId(id) } }
               ).FirstOrDefaultAsync().Result;

            return balance;

        }

    }
}