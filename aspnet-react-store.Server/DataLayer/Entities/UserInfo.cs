namespace aspnet_react_store.Server.Entities {
    public class UserInfo {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime JoinDate { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}