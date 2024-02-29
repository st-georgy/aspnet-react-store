using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface ICartsService
    {
        Task<Cart> GetUserCart(int userId);
    }
}