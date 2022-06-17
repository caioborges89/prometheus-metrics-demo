using MongoDB.Driver;
using Prometheus.Metrics.Demo.Model;

namespace Prometheus.Metrics.Demo.infra.interfaces
{
    public interface IContext
    {
        IMongoCollection<OrderModel> Orders { get; }
    }
}