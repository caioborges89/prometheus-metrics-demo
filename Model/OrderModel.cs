using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Prometheus.Metrics.Demo.Model
{
    public class OrderModel
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;

        public decimal Total { get; set; }

        public StatusModel Status { get; set; }

        public List<OrderItemModel>? Items { get; set; }
    }

    public class OrderItemModel
    {
        public string? Id { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public decimal Value { get; set; }
    }
}