using CSharpFunctionalExtensions;
using aspnet_react_store.Domain.Models.Enums;
using aspnet_react_store.Domain.Models.Linking;

namespace aspnet_react_store.Domain.Models
{
    public class Order
    {
        public int Id { get; }
        public decimal TotalPrice { get; }
        public int TotalProducts { get; }
        public decimal Discount { get; }
        public OrderStatus OrderStatus { get; }
        public List<ProductOrder> ProductOrders { get; }

        private Order(int id,
            decimal totalPrice,
            int totalProducts,
            decimal discount,
            OrderStatus orderStatus,
            List<ProductOrder> productOrders)
        {
            Id = id;
            TotalPrice = totalPrice;
            TotalProducts = totalProducts;
            Discount = discount;
            OrderStatus = orderStatus;
            ProductOrders = productOrders;
        }

        public static Result<Order> Create(int id,
            decimal totalPrice,
            int totalProducts,
            decimal discount,
            List<ProductOrder> productOrders,
            OrderStatus orderStatus = OrderStatus.Pending)
        {
            if (id < 0)
                return Result.Failure<Order>("Id must be greater than 0.");

            if (totalPrice < 0)
                return Result.Failure<Order>("TotalPrice must not be lower than 0.");

            if (totalProducts < 0)
                return Result.Failure<Order>("TotalProducts must not be lower than 0.");

            if (discount > 1 || discount < 0)
                return Result.Failure<Order>("Discount must be between 0 and 1.");

            var order = new Order(id, totalPrice, totalProducts, discount, orderStatus, productOrders);

            return Result.Success(order);
        }
    }
}
