using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Prometheus.Metrics.Demo.infra.interfaces;
using Prometheus.Metrics.Demo.Model;

namespace Prometheus.Metrics.Demo.repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IContext _context;

        public OrderRepository(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(OrderModel order)
        {
            await _context.Orders.InsertOneAsync(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Orders.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (result is null)
                throw new Exception(nameof(result));

            var deleteResult = await _context.Orders.DeleteOneAsync(x => x.Id == result.Id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            return await _context.Orders.Find(_ => true).ToListAsync();
        }

        public async Task<int> GetNextIdAsync()
        {
            var result = await _context.Orders.CountDocumentsAsync(new BsonDocument()) + 1;
            return (int)result;
        }

        public async Task<OrderModel> GetOrderByIdAsync(int id)
        {
            var result = await _context.Orders.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (result is null)
                throw new Exception(nameof(result));

            return result;
        }

        public async Task<bool> UpdateAsync(OrderModel order)
        {
            ReplaceOneResult updateResult = await _context.Orders.ReplaceOneAsync(filter: g => g.Id == order.Id, replacement: order);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}