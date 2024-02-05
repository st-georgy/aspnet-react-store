using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Image
    {
        public int Id { get; }
        public string FilePath { get; } = null!;

        private Image(int id, string filePath)
        {
            Id = id;
            FilePath = filePath;
        }

        public static Result<Image> Create(int id, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return Result.Failure<Image>("FilePath can not be null or empty");

            var image = new Image(id, filePath);

            return Result.Success(image);
        }
    }
}