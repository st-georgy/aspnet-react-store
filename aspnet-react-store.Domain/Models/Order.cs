using aspnet_react_store.Domain.Models.Enums;
using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Order
    {
        public int Id { get; }
        public OrderStatus OrderStatus { get; }
        public User? User { get; }
        public List<Product> Products { get; } = [];

        private Order(int id, OrderStatus orderStatus, User? user)
        {
            Id = id;
            OrderStatus = orderStatus;
            User = user;
        }

        public static Result<Order> Create(int Id, User? user, OrderStatus orderStatus = OrderStatus.Pending)
        {
            if (user is null)
                return Result.Failure<Order>("Invalid User: User can not be null");

            var order = new Order(Id, orderStatus, user);

            return Result.Success(order);
        }
    }
}
