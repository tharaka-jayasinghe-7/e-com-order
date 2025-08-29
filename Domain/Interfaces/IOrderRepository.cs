using Domain.Entities;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> AddOrderAsync(Order order);
    Task<Order?> GetOrderByIdAsync(string id);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(string id);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
}