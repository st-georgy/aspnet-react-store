using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface ICategoriesRepository
    {
        Task<int> Create(Category category);
        Task<int> Delete(int id);
        Task<IEnumerable<Category>> Get();
        Task<Category> Get(int id);
        Task<int> Update(int id, string name);
    }
}