using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Cart
    {
        public int Id { get; }
        public List<Product> Products { get; } = [];

        private Cart(int id)
        {
            Id = id;
        }

        public static Result<Cart> Create(int id)
        {
            var cart = new Cart(id);

            return Result.Success(cart);
        }
    }
}
