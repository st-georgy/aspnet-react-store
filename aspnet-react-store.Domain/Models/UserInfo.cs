using CSharpFunctionalExtensions;

namespace aspnet_react_store.Domain.Models
{
    public class UserInfo
    {
        public int Id { get; }
        public string? FirstName { get; }
        public string? MiddleName { get; }
        public string? LastName { get; }
        public DateTime JoinDate { get; }

        private UserInfo(int id, string? firstName, string? middleName, string? lastName, DateTime joinDate)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            JoinDate = joinDate;
        }

        public static Result<UserInfo> Create(int id, string? firstName, string? middleName, string? lastName, DateTime? joinDate)
        {
            joinDate ??= DateTime.Now;
            var userInfo = new UserInfo(id, firstName, middleName, lastName, joinDate.Value);

            return Result.Success(userInfo);
        }
    }
}
