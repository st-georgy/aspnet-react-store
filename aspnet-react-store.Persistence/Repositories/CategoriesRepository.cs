using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Repositories
{
    public class CategoriesRepository(StoreDbContext context, IMapper mapper) : ICategoriesRepository
    {
        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Category>> Get()
        {
            var categoryEntities = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return categoryEntities.Select(c => _mapper.Map<Category>(c)!);
        }

        public async Task<Category> Get(int id)
        {
            var categoryEntity = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<Category>(categoryEntity)!;
        }

        public async Task<int> Create(Category category)
        {
            var categoryEntity = _mapper.Map<CategoryEntity>(category)!;

            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();

            return categoryEntity.Id;
        }

        public async Task<int> Update(int id, string name)
        {
            await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(c => c.Name, c => name));

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
