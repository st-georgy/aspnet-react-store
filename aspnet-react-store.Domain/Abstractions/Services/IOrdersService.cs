using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Models.Enums;

namespace aspnet_react_store.Domain.Abstractions.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAllUserOrders(int userId);
        Task<int> CreateOrder(Order order);
        Task<int> UpdateOrderStatus(int id, OrderStatus status);
        Task<int> DeleteOrder(int id);
    }
}