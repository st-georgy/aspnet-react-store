using aspnet_react_store.Persistence.Entities.Enums;
using aspnet_react_store.Persistence.Entities.Linking;

namespace aspnet_react_store.Persistence.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }

        public OrderStatusEnum OrderStatus { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public List<ProductOrderEntity> ProductOrders = [];
    }
}