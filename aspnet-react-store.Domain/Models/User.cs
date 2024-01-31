using aspnet_react_store.Domain.Models.Enums;
using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class User
    {
        public int Id { get; }
        public string UserName { get; } = null!;
        public string PasswordHash { get; } = null!;
        public AccountType AccountType { get; }
        public Cart? Cart { get; }
        public UserInfo? UserInfo { get; }
        public List<Order> Orders { get; } = [];

        private User(int id,
            string userName,
            string passwordHash,
            Cart? cart,
            UserInfo? userInfo,
            AccountType accountType)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Cart = cart;
            UserInfo = userInfo;
            AccountType = accountType;
        }

        public static Result<User> Create(int Id,
            string userName,
            string passwordHash,
            Cart? cart,
            UserInfo? userInfo,
            AccountType accountType = AccountType.User)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return Result.Failure<User>("UserName can not be null or empty");

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>("PasswordHash can not be null or empty");

            var order = new User(Id, userName, passwordHash, cart, userInfo, accountType);

            return Result.Success(order);
        }
    }
}
