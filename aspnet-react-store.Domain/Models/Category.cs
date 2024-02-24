using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class Category
    {
        public int Id { get; }
        public string Name { get; }

        private Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Category> Create(int id, string name)
        {
            if (id < 0)
                return Result.Failure<Category>("Id must be greater than 0.");

            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Category>("Category Name can not be null or white space.");

            var category = new Category(id, name);
            return Result.Success(category);
        }
    }
}