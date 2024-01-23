namespace aspnet_react_store.Server.Entities {
    public class UserInfo {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public string? AvatarPath { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}