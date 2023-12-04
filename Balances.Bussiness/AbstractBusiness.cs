﻿using AutoMapper;
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

        public Balance GetBalance()
        {

            var balanceId = _sessionService.GetBalanceId();


            var balance = _balances.Find(
                new BsonDocument { { "_id", new ObjectId(balanceId) } }
               ).FirstOrDefaultAsync().Result;

            return balance;

        }

    }
}