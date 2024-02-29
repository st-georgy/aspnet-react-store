using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Cart
    {
        public int Id { get; }
        public decimal Discount { get; }

        private Cart(int id, decimal discount)
        {
            Id = id;
            Discount = discount;
        }

        public static Result<Cart> Create(int id, decimal discount)
        {
            if (id < 0)
                return Result.Failure<Cart>("Id must be greater than 0.");

            if (discount > 1 || discount < 0)
                return Result.Failure<Cart>("Discount must be between 0 and 1.");

            var cart = new Cart(id, discount);

            return Result.Success(cart);
        }
    }
}
