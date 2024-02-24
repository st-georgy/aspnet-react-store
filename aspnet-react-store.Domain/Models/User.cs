using CSharpFunctionalExtensions;
using aspnet_react_store.Domain.Models.Enums;

namespace aspnet_react_store.Domain.Models
{
    public class User
    {
        public int Id { get; }
        public string UserName { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public UserRole UserRole { get; }

        public UserInfo? UserInfo { get; }
        public Cart? Cart { get; }

        public List<Order> Orders { get; } = [];
        public List<Product> Products { get; } = [];

        private User(int id,
            string userName,
            string email,
            string passwordHash,
            UserInfo? userInfo,
            Cart? cart,
            List<Order> orders,
            List<Product> products,
            UserRole userRole)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            UserRole = userRole;
            UserInfo = userInfo;
            Cart = cart;
            Orders = orders;
            Products = products;
        }

        public static Result<User> Create(int id,
            string userName,
            string email,
            string passwordHash,
            UserInfo? userInfoId,
            Cart? cartId,
            List<Order> orders,
            List<Product> products,
            UserRole userRole = UserRole.User)
        {
            if (id < 0)
                return Result.Failure<User>("Id must not be less than 0.");

            if (string.IsNullOrWhiteSpace(userName))
                return Result.Failure<User>("UserName can not be null or empty");

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>("PasswordHash can not be null or empty");

            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<User>("Email can not be null or empty");

            var order = new User(id, userName, email, passwordHash, userInfoId, cartId, orders, products, userRole);

            return Result.Success(order);
        }
    }
}
