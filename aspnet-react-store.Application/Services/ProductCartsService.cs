using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Models.Linking;

namespace aspnet_react_store.Application.Services
{
    public class ProductCartsService(IProductCartsRepository productCartsRepository)
        : IProductCartsService
    {
        private readonly IProductCartsRepository _productCartsRepository = productCartsRepository;

        public async Task<IEnumerable<ProductCart>> GetProductsInCart(int cartId) =>
            await _productCartsRepository.Get(cartId);

        public async Task<int> AddProductToCart(int cartId, int productId) =>
            await _productCartsRepository.AddToCart(cartId, productId);

        public async Task<int> RemoveProductFromCart(int cartId, int productId) =>
            await _productCartsRepository.RemoveFromCart(cartId, productId);
    }
}