using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface IProductsRepository
    {
        Task<int> Create(Product product);
        Task<int> Delete(int id);
        Task<IEnumerable<Product>> Get();
        Task<IEnumerable<Product>> Get(int? startId, int? count, string? searchText);
        Task<int> Update(int id, string name, decimal price, string description);
    }
}
