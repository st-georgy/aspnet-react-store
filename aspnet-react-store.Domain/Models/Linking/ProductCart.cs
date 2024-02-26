using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models.Linking
{
    public class ProductCart
    {
        public int Quantity { get; }
        public Product? Product { get; }
        public Cart? Cart { get; }

        private ProductCart(int quantity, Product? product, Cart? cart)
        {
            Quantity = quantity;
            Product = product;
            Cart = cart;
        }

        public static Result<ProductCart> Create(int quantity, Product? product, Cart? cart)
        {
            if (quantity < 1)
                return Result.Failure<ProductCart>("Quantity can not be less than 1.");

            var productCart = new ProductCart(quantity, product, cart);

            return Result.Success(productCart);
        }
    }
}