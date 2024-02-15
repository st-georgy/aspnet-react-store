using aspnet_react_store.Domain.Abstractions.Repositories;
using aspnet_react_store.Domain.Abstractions.Services;
using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Models.Enums;

namespace aspnet_react_store.Application.Services
{
    public class OrdersService(IOrdersRepository ordersRepository) : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository = ordersRepository;

        public async Task<IEnumerable<Order>> GetAllUserOrders(int userId) =>
            await _ordersRepository.Get(userId);

        public async Task<int> CreateOrder(Order order) =>
            await _ordersRepository.Create(order);

        public async Task<int> UpdateOrderStatus(int id, OrderStatus status) =>
            await _ordersRepository.Update(id, status);

        public async Task<int> DeleteOrder(int id) =>
            await _ordersRepository.Delete(id);
    }
}