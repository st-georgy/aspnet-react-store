using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface ICartsRepository
    {
        Task<Cart> Get(int userId);
        Task<int> Add(int userId, int productId);
        Task<int> Remove(int userId, int productId);
    }
}