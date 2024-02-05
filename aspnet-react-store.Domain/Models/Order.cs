using CSharpFunctionalExtensions;
using aspnet_react_store.Domain.Models.Enums;

namespace aspnet_react_store.Domain.Models
{
    public class Order
    {
        public int Id { get; }
        public OrderStatus OrderStatus { get; }
        public User User { get; }
        public List<Product> Products { get; } = [];

        private Order(int id, OrderStatus orderStatus, User user)
        {
            Id = id;
            OrderStatus = orderStatus;
            User = user;
        }

        public static Result<Order> Create(int id, User? user, OrderStatus orderStatus = OrderStatus.Pending)
        {
            if (user is null)
                return Result.Failure<Order>("Invalid User: User can not be null");

            var order = new Order(id, orderStatus, user);

            return Result.Success(order);
        }
    }
}
