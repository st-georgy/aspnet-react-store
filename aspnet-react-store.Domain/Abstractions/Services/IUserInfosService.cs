using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface IUserInfosService
    {
        Task<int> DeleteUserInfo(int userId);
        Task<UserInfo> GetUserInfo(int userId);
        Task<int> UpdateUserInfo(int userId, string? firstName, string? middleName, string? lastName);
    }
}