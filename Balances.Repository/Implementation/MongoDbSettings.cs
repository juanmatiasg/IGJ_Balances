using Balances.Repository.Contract;

namespace Balances.Repository.Implementation
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
