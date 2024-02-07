using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task Add(User user);
        Task<int> GetNextId();
    }
}