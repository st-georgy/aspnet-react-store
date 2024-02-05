﻿using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Mapping;

namespace aspnet_react_store.Persistence.Repositories
{
    public class ProductsRepository(StoreDbContext context) : IProductsRepository
    {

        private readonly StoreDbContext _context = context;

        public async Task<IEnumerable<Product>> Get()
        {
            var productEntities = await _context.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .ToListAsync();

            return productEntities.Select(Mapper.Map);
        }

        public async Task<int> Create(Product product)
        {
            var productEntity = new ProductEntity
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            return productEntity.Id;
        }

        public async Task<int> Update(int id, string name, decimal price, string description)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(p => p.Name, p => name)
                    .SetProperty(p => p.Price, p => price)
                    .SetProperty(p => p.Description, p => description));

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
            var query = _context.Products.Include(p => p.Images).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var searchTerms = searchText.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                query = query.Where(p =>
                    p.Name.ToLower().Equals(searchText)
                        || searchTerms.Any(term =>
                            p.Name.ToLower().Contains(term)));
            }

            if (!startId.HasValue || !count.HasValue)
                return (await query.ToListAsync()).Select(Mapper.Map);

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

            return result.Select(Mapper.Map);
        }
    }
}