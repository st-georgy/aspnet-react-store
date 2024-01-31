using aspnet_react_store.Domain.Abstractions;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Application.Services
{
    public class ProductsService(IProductsRepository productsRepository) : IProductsService
    {
        private readonly IProductsRepository _productsRepository = productsRepository;

        public async Task<IEnumerable<Product>> GetAllProducts() =>
            await _productsRepository.Get();

        public async Task<IEnumerable<Product>> GetProducts(int? startId, int? count, string? searchText) =>
            await _productsRepository.Get(startId, count, searchText);

        public async Task<int> CreateProduct(Product product) =>
            await _productsRepository.Create(product);

        public async Task<int> UpdateProduct(int id, string name, decimal price, string description) =>
            await _productsRepository.Update(id, name, price, description);

        public async Task<int> DeleteProduct(int id) =>
            await _productsRepository.Delete(id);
    }
}
