using CSharpFunctionalExtensions;
using aspnet_react_store.Domain.Models.Enums;
using aspnet_react_store.Domain.Models.Linking;

namespace aspnet_react_store.Domain.Models
{
    public class Order
    {
        public int Id { get; }
        public decimal Discount { get; }
        public OrderStatus OrderStatus { get; }
        public List<ProductOrder> ProductOrders { get; }

        private Order(int id,
            decimal discount,
            OrderStatus orderStatus,
            List<ProductOrder> productOrders)
        {
            Id = id;
            Discount = discount;
            OrderStatus = orderStatus;
            ProductOrders = productOrders;
        }

        public static Result<Order> Create(int id,
            decimal discount,
            List<ProductOrder> productOrders,
            OrderStatus orderStatus = OrderStatus.Pending)
        {
            if (id < 0)
                return Result.Failure<Order>("Id must be greater than 0.");

            if (discount > 1 || discount < 0)
                return Result.Failure<Order>("Discount must be between 0 and 1.");

            var order = new Order(id, discount, orderStatus, productOrders);

            return Result.Success(order);
        }
    }
}
