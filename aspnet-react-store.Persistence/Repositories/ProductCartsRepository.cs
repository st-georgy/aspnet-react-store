using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Exceptions;
using aspnet_react_store.Domain.Models.Linking;
using aspnet_react_store.Persistence.Entities.Linking;

namespace aspnet_react_store.Persistence.Repositories
{
    public class ProductCartsRepository(StoreDbContext context, IMapper mapper)
        : IProductCartsRepository
    {
        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<ProductCart>> Get(int cartId)
        {
            var productCarts = await _context.ProductCarts
                .Where(pc => pc.CartId == cartId)
                .Include(pc => pc.Cart)
                .Include(pc => pc.Product)
                    .ThenInclude(p => p.Images)
                .ToListAsync();

            return productCarts.Select(pc => _mapper.Map<ProductCart>(pc)!);
        }

        public async Task<int> AddToCart(int cartId, int productId)
        {
            var quantity = 1;

            var productCart = await GetProductCart(cartId, productId);

            if (productCart is not null)
                quantity = ++productCart.Quantity;
            else
                await _context.ProductCarts.AddAsync(new ProductCartEntity { CartId = cartId, ProductId = productId, Quantity = 1 });

            await _context.SaveChangesAsync();
            return quantity;
        }

        public async Task<int> RemoveFromCart(int cartId, int productId)
        {
            var quantity = 0;

            var productCart = await GetProductCart(cartId, productId);

            if (productCart is not null)
            {
                if (productCart.Quantity == 1)
                    _context.ProductCarts.Remove(productCart);
                else
                    quantity = ++productCart.Quantity;

                await _context.SaveChangesAsync();
            }

            return quantity;
        }

        private async Task<ProductCartEntity?> GetProductCart(int cartId, int productId)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId)
                    ?? throw new EntityNotFoundException("Product not found");

            var productCart = await _context.ProductCarts
                .FirstOrDefaultAsync(pc => pc.CartId == cartId
                    && pc.ProductId == productId);

            return productCart;
        }
    }
}