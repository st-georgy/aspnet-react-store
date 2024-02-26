using AutoMapper;
using Microsoft.EntityFrameworkCore;
using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Models.Enums;
using aspnet_react_store.Persistence.Entities;
using aspnet_react_store.Persistence.Entities.Enums;

namespace aspnet_react_store.Persistence.Repositories
{
    public class OrdersRepository(StoreDbContext context, IMapper mapper) : IOrdersRepository
    {
        private readonly StoreDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Order>> Get(int userId)
        {
            var orderEntities = await _context.Orders
                .AsNoTracking()
                .Include(o => o.ProductOrders)
                    .ThenInclude(po => po.Order)
                .Where(o => o.UserId == userId)
                .AsSplitQuery()
                .ToListAsync();

            return orderEntities.Count == 0 ? [] :
                orderEntities.Select(o => _mapper.Map<Order>(o)!);
        }

        public async Task<int> Create(Order order)
        {
            var orderEntity = _mapper.Map<OrderEntity>(order)!;

            await _context.Orders.AddAsync(orderEntity);
            await _context.SaveChangesAsync();

            return orderEntity.Id;
        }

        public async Task<int> Update(int id, OrderStatus status)
        {
            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(o => o.OrderStatus, o => (OrderStatusEnum)status));

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}