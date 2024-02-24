using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Application.Services
{
    public class CategoriesService(ICategoriesRepository categoriesRepository) : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository = categoriesRepository;

        public async Task<IEnumerable<Category>> GetAll()
            => await _categoriesRepository.Get();

        public async Task<Category> Get(int id)
            => await _categoriesRepository.Get(id);

        public async Task<int> Create(Category category)
            => await _categoriesRepository.Create(category);

        public async Task<int> Update(int id, string name)
            => await _categoriesRepository.Update(id, name);

        public async Task<int> Delete(int id)
            => await _categoriesRepository.Delete(id);
    }
}
