using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models.Linking
{
    public class ProductOrder
    {
        public int Quantity { get; }
        public Product? Product { get; }
        public Order? Order { get; }

        private ProductOrder(int quantity, Product? product, Order? order)
        {
            Quantity = quantity;
            Product = product;
            Order = order;
        }

        public static Result<ProductOrder> Create(int quantity, Product? product, Order? order)
        {
            if (quantity < 1)
                return Result.Failure<ProductOrder>("Quantity can not be less than 1.");

            var productOrder = new ProductOrder(quantity, product, order);

            return Result.Success(productOrder);
        }
    }
}