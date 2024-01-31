using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Cart
    {
        public int Id { get; }
        public User? User { get; }
        public List<Product> Products { get; } = [];

        private Cart(int id, User user)
        {
            Id = id;
            User = user;
        }

        public static Result<Cart> Create(int id, User? user)
        {
            if (user is null)
                return Result.Failure<Cart>("User can not be null");

            var cart = new Cart(id, user);

            return Result.Success(cart);
        }
    }
}
