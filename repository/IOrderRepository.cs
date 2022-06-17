using Prometheus.Metrics.Demo.Model;

namespace Prometheus.Metrics.Demo.repository
{
    public interface IOrderRepository
    {
        // api/[GET]
        Task<IEnumerable<OrderModel>> GetAllAsync();
        // api/1/[GET]
        Task<OrderModel> GetOrderByIdAsync(int id);
        // api/[POST]
        Task CreateAsync(OrderModel order);
        // api/[PUT]
        Task<bool> UpdateAsync(OrderModel order);
        // api/1/[DELETE]
        Task<bool> DeleteAsync(int id);

        Task<int> GetNextIdAsync();
    }
}