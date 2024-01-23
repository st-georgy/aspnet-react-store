using aspnet_react_store.Server.Entities.Enums;

namespace aspnet_react_store.Server.Entities {
    public class Order {
        public int Id { get; set; }

        public OrderStatusEnum OrderStatus { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }

        public List<Product> Products = [];
    }
}