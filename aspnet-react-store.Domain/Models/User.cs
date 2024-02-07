using CSharpFunctionalExtensions;
using aspnet_react_store.Domain.Models.Enums;

namespace aspnet_react_store.Domain.Models
{
    public class User
    {
        public int Id { get; }
        public string UserName { get; } = null!;
        public string Email { get; } = null!;
        public string PasswordHash { get; } = null!;
        public UserRole UserRole { get; }
        public Cart? Cart { get; }
        public UserInfo? UserInfo { get; }

        private User(int id,
            string userName,
            string email,
            string passwordHash,
            Cart? cart,
            UserInfo? userInfo,
            UserRole userRole)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Cart = cart;
            UserInfo = userInfo;
            UserRole = userRole;
        }

        public static Result<User> Create(int id,
            string userName,
            string email,
            string passwordHash,
            Cart? cart,
            UserInfo? userInfo,
            UserRole userRole = UserRole.User)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return Result.Failure<User>("UserName can not be null or empty");

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>("PasswordHash can not be null or empty");

            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<User>("Email can not be null or empty");

            var order = new User(id, userName, email, passwordHash, cart, userInfo, userRole);

            return Result.Success(order);
        }
    }
}
