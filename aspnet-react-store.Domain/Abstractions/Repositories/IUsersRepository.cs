using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task Create(User user);
        Task<int> Update(int id, string? username, string? email);
        Task<int> UpdatePassword(int id, string passwordHash);
    }
}