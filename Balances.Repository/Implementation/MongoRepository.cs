﻿using Balances.Model;
using Balances.Repository.Contract;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Balances.Repository.Implementation
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<Balance> _balances;


        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _balances = database.GetCollection<Balance>(collectionName);
        }

        public List<Balance> GetAll()
        {

            return _balances.Find(p => true).ToList();
        }

        public Balance GetBalance(string id)
        {
            var balance = _balances.Find(
                 new BsonDocument { { "_id", new ObjectId(id) } }
                ).FirstOrDefaultAsync().Result;

            return balance;
        }


        public void UpdateBalance(string id, Balance balance)
        {
            _balances.ReplaceOneAsync(b => b.Id == id, balance);
        }

        public void DeleteBalance(string id)
        {

            _balances.DeleteOneAsync(d => d.Id == id);
        }

        public void CreateBalance(Balance balance)
        {
            _balances.InsertOneAsync(balance);
        }

        public Balance Create(Balance balance)
        {
            throw new NotImplementedException();
        }

        public Balance GetById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, Balance balance)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
