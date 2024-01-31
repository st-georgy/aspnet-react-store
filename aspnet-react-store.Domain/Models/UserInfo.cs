using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class UserInfo
    {
        public int Id { get; }
        public string? Email { get; }
        public string? FirstName { get; }
        public string? MiddleName { get; }
        public string? LastName { get; }
        public DateTime JoinDate { get; }

        private UserInfo(int id, string? email, string? firstName, string? middleName, string? lastName, DateTime joinDate)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            JoinDate = joinDate;
        }

        public static Result<UserInfo> Create(int id, string? email, string? firstName, string? middleName, string? lastName, DateTime? joinDate)
        {
            joinDate ??= DateTime.Now;
            var userInfo = new UserInfo(id, email, firstName, middleName, lastName, joinDate.Value);

            return Result.Success(userInfo);
        }
    }
}
