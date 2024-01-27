using aspnet_react_store.Server.Entities.Enums;

namespace aspnet_react_store.Server.Entities {
    public class User {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public AccountTypeEnum AccountType { get; set; }
        
        public Cart? Cart { get; set; }
        public UserInfo? UserInfo { get; set; }

        public List<Order> Orders { get; set; } = [];
    }
}