using aspnet_react_store.Domain.Models.Linking;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface IProductCartsRepository
    {
        Task<IEnumerable<ProductCart>> Get(int cartId);
        Task<int> AddToCart(int userId, int productId);
        Task<int> RemoveFromCart(int userId, int productId);
    }
}