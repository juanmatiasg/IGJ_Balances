namespace Balances.Repository.Contract
{
    public interface IMongoDbSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
