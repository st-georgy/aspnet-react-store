using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Exceptions;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Persistence.Entities.Linking;

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
                .Include(c => c.ProductCarts)
                    .ThenInclude(pc => pc.Product)
                .Where(c => c.UserId == userId)
                .AsSplitQuery()
                .ToListAsync();

            return _mapper.Map<Cart>(cartEntity)!;
        }

        public async Task<int> AddToCart(int userId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.ProductCarts)
                    .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId)
                    ?? throw new EntityNotFoundException("User's cart not found.");

            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId)
                    ?? throw new EntityNotFoundException("Product not found.");

            var productCart = cart!.ProductCarts.FirstOrDefault(pc => pc.ProductId == productId);

            if (productCart is not null)
                productCart.Quantity++;
            else
                cart!.ProductCarts.Add(new ProductCartEntity { ProductId = productId, Quantity = 1 });

            await _context.SaveChangesAsync();

            return userId;
        }

        public async Task<int> RemoveFromCart(int userId, int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.ProductCarts)
                .FirstOrDefaultAsync(c => c.UserId == userId)
                    ?? throw new EntityNotFoundException("User's cart not found.");

            var productCart = cart.ProductCarts.FirstOrDefault(po => po.ProductId == productId);
            if (productCart is not null)
            {
                if (productCart.Quantity > 1)
                    productCart.Quantity--;
                else
                    cart.ProductCarts.Remove(productCart);
            }
            else return 0;

            await _context.SaveChangesAsync();

            return userId;
        }
    }
}