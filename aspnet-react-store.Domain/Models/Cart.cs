using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Cart
    {
        public int Id { get; }
        public decimal TotalPrice { get; }
        public int TotalProducts { get; }
        public decimal Discount { get; }
        public List<Product> Products { get; }

        private Cart(int id, decimal totalPrice, int totalProducts, decimal discount, List<Product> products)
        {
            Id = id;
            TotalPrice = totalPrice;
            TotalProducts = totalProducts;
            Discount = discount;
            Products = products;
        }

        public static Result<Cart> Create(int id, decimal totalPrice, int totalProducts, decimal discount, List<Product> products)
        {
            if (id < 0)
                return Result.Failure<Cart>("Id must be greater than 0.");

            if (totalPrice < 0)
                return Result.Failure<Cart>("TotalPrice must not be lower than 0.");

            if (totalProducts < 0)
                return Result.Failure<Cart>("TotalProducts must not be lower than 0.");

            if (discount > 1 || discount < 0)
                return Result.Failure<Cart>("Discount must be between 0 and 1.");

            var cart = new Cart(id, totalPrice, totalProducts, discount, products);

            return Result.Success(cart);
        }
    }
}
