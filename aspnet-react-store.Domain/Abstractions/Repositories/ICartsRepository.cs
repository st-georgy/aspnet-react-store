using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface ICartsRepository
    {
        Task<Cart> Get(int userId);
    }
}