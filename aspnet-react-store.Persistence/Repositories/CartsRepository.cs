using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Exceptions;
using aspnet_react_store.Domain.Models;

namespace aspnet_react_store.Persistence.Repositories
{
    public class CartsRepository(StoreDbContext context, IMapper mapper) : ICartsRepository
    {
        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Cart> Get(int userId)
        {
            var cartEntity = await _context.Carts
                .AsNoTracking()
                .Include(c => c.Products)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return _mapper.Map<Cart>(cartEntity)!;
        }

        public async Task<int> Add(int userId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId)
                    ?? throw new EntityNotFoundException("User's cart not found.");

            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId)
                    ?? throw new EntityNotFoundException("Product not found.");

            cart!.Products.Add(product);

            await _context.SaveChangesAsync();

            return userId;
        }

        public async Task<int> Remove(int userId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.UserId == userId)
                    ?? throw new EntityNotFoundException("User's cart not found.");

            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product is null) return 0;

            cart.Products.Remove(product);

            await _context.SaveChangesAsync();

            return userId;
        }
    }
}