using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface IUserInfosRepository
    {
        Task<int> Delete(int userId);
        Task<UserInfo> Get(int userId);
        Task<int> Update(int userId, string? firstName, string? middleName, string? lastName);
    }
}