using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Exceptions;

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
                .FirstOrDefaultAsync(c => c.UserId == userId)
                    ?? throw new EntityNotFoundException("User's cart not found");

            return _mapper.Map<Cart>(cartEntity)!;
        }
    }
}