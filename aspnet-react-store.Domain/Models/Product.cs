using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; } = null!;
        public decimal Price { get; }
        public string? Description { get; }

        private Product(int id, string name, decimal price, string? description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }

        public static Result<Product> Create(int id, string name, decimal price, string? description)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Product>("Product name can not be null");

            if (price <= 0)
                return Result.Failure<Product>("Price can not be <= 0");

            var product = new Product(id, name, price, description);

            return Result.Success(product);
        }
    }
}
