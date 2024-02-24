using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Application.Services
{
    public class CartsService(ICartsRepository cartsRepository) : ICartsService
    {
        private readonly ICartsRepository _cartsRepository = cartsRepository;

        public async Task<Cart> GetUserCart(int userId) =>
            await _cartsRepository.Get(userId);

        public async Task<int> AddProductToCart(int userId, int productId) =>
            await _cartsRepository.AddToCart(userId, productId);

        public async Task<int> RemoveProductFromCart(int userId, int productId) =>
            await _cartsRepository.RemoveFromCart(userId, productId);
    }
}