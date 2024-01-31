using aspnet_react_store.DataAccess.Entities.Enums;

namespace aspnet_react_store.DataAccess.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public OrderStatusEnum OrderStatus { get; set; }

        public int UserId { get; set; }
        public UserEntity? User { get; set; }

        public List<ProductEntity> Products = [];
    }
}