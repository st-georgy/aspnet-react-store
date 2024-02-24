using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Exceptions;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Persistence.Entities;

namespace aspnet_react_store.Persistence.Repositories
{
    public class ProductsRepository(StoreDbContext context, IMapper mapper) : IProductsRepository
    {

        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Product> Get(int id)
        {
            var productEntity = await _context.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id)
                    ?? throw new EntityNotFoundException("Product not found");

            return _mapper.Map<Product>(productEntity)!;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            var productEntities = await _context.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .Include(p => p.Categories)
                .ToListAsync();

            return productEntities.Select(p => _mapper.Map<Product>(p)!);
        }

        public async Task<int> Create(Product product)
        {
            var productEntity = _mapper.Map<ProductEntity>(product)!;

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            return productEntity.Id;
        }

        public async Task<int> Update(int id, string? name, decimal? price, string? description)
        {
            if (name is null && price is null && description is null)
                return 0;

            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(p => p.Name, p => name ?? p.Name)
                    .SetProperty(p => p.Price, p => price ?? p.Price)
                    .SetProperty(p => p.Description, p => description ?? p.Description));

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<IEnumerable<Product>> Get(int? startId, int? count, string? searchText)
        {
            var query = _context.Products
                .Include(p => p.Images)
                .Include(p => p.Categories)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var searchTerms = searchText.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
                query = query.Where(p =>
                    p.Name.ToLower().Equals(searchText)
                        || searchTerms.Any(term =>
                            p.Name.ToLower().Contains(term)));
#pragma warning restore CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons
            }

            if (!startId.HasValue || !count.HasValue)
                return (await query.ToListAsync()).Select(p => _mapper.Map<Product>(p)!);

            if (startId < 1 || count < 1)
                throw new ArgumentException("Invalid arguments (startId or count)");

            var maxProductId = await _context.Products.MaxAsync(p => (int?)p.Id) ?? 0;

            if (startId > maxProductId)
                throw new ArgumentOutOfRangeException(nameof(startId), " exceeds the maximum Id in the database");

            query = query
                .OrderBy(p => p.Id)
                .Where(p => p.Id >= startId)
                .Take(count.Value);

            var result = await query.ToListAsync();

            return result.Select(p => _mapper.Map<Product>(p)!);
        }
    }
}
