using aspnet_react_store.DataAccess.Entities.Enums;

namespace aspnet_react_store.DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public AccountTypeEnum AccountType { get; set; }

        public CartEntity? Cart { get; set; }
        public UserInfoEntity? UserInfo { get; set; }

        public List<OrderEntity> Orders { get; set; } = [];
    }
}