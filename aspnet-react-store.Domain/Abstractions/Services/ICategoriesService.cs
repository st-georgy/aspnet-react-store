using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface ICategoriesService
    {
        Task<int> Create(Category category);
        Task<int> Delete(int id);
        Task<Category> Get(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<int> Update(int id, string name);
    }
}