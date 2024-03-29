using aspnet_react_store.Persistence.Entities.Enums;

namespace aspnet_react_store.Persistence.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public UserRoleEnum UserRole { get; set; }

        public CartEntity? Cart { get; set; }
        public UserInfoEntity? UserInfo { get; set; }

        public List<OrderEntity> Orders { get; set; } = [];
        public List<ProductEntity> Products { get; set; } = [];
    }
}