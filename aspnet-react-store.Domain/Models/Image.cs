using aspnet_react_store.Domain.Models;
using CSharpFunctionalExtensions;

namespace aspnet_react_store.Core.Models
{
    public class Image
    {
        public int Id { get; }
        public string FilePath { get; } = null!;
        public Product Product { get; }

        private Image(int id, string filePath, Product product)
        {
            Id = id;
            FilePath = filePath;
            Product = product;
        }

        public static Result<Image> Create(int id, string filePath, Product product)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return Result.Failure<Image>("FilePath can not be null or empty");

            var image = new Image(id, filePath, product);

            return Result.Success(image);
        }
    }
}