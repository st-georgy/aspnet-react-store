using aspnet_react_store.Domain.Models;
using aspnet_react_store.Domain.Models.Enums;

namespace aspnet_react_store.Domain.Abstractions.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> Get(int userId);
        Task<int> Create(Order order);
        Task<int> Update(int id, OrderStatus status);
        Task<int> Delete(int id);
    }
}