using aspnet_react_store.Domain.Models.Linking;

namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface IProductCartsService
    {
        Task<IEnumerable<ProductCart>> GetProductsInCart(int cartId);
        Task<int> AddProductToCart(int cartId, int productId);
        Task<int> RemoveProductFromCart(int cartId, int productId);
    }
}