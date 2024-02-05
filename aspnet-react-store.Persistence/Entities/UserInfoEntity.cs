namespace aspnet_react_store.Persistence.Entities
{
    public class UserInfoEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime JoinDate { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}