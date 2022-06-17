using MongoDB.Driver;
using Prometheus.Metrics.Demo.infra.interfaces;
using Prometheus.Metrics.Demo.Model;

namespace Prometheus.Metrics.Demo.infra
{
    public class MongoDbContext : IContext
    {
        private readonly IMongoDatabase _db;
        public MongoDbContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<OrderModel> Orders => _db.GetCollection<OrderModel>("Orders");
    }
}