using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public decimal Discount { get; }
        public string? Description { get; }
        public List<Category> Categories { get; }
        public List<Image> Images { get; }

        private Product(int id,
            string name,
            decimal price,
            int quantity,
            decimal discount,
            string? description,
            List<Category> categories,
            List<Image> images)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Discount = discount;
            Description = description;
            Categories = categories;
            Images = images;
        }

        public static Result<Product> Create(int id,
            string name,
            decimal price,
            int quantity,
            decimal discount,
            string? description,
            List<Category> categories,
            List<Image> images)
        {
            if (id < 0)
                return Result.Failure<Product>("Id must be greater than 0.");

            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Product>("Product Name can not be null or white space.");

            if (price < 0)
                return Result.Failure<Product>("Price must not be lower than 0.");

            if (quantity < 0)
                return Result.Failure<Product>("Quantity must not be lower than 0.");

            if (discount > 1 || discount < 0)
                return Result.Failure<Product>("Discount must be between 0 and 1.");

            var product = new Product(id, name, price, quantity, discount, description, categories, images);

            return Result.Success(product);
        }
    }
}
