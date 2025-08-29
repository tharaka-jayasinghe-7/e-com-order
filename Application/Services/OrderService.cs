using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto dto)
    {
        var order = new Order
        {
            UserId = dto.UserId,
            ProductIds = dto.ProductIds,
            TotalAmount = dto.TotalAmount,
            Status = "Pending"
        };

        var created = await _repository.AddOrderAsync(order);

        return new OrderDto
        {
            Id = created.Id,
            UserId = created.UserId,
            ProductIds = created.ProductIds,
            TotalAmount = created.TotalAmount,
            Status = created.Status
        };
    }

    public async Task<OrderDto?> GetOrderAsync(string id)
    {
        var order = await _repository.GetOrderByIdAsync(id);
        if (order == null) return null;

        return new OrderDto
        {
            Id = order.Id,
            UserId = order.UserId,
            ProductIds = order.ProductIds,
            TotalAmount = order.TotalAmount,
            Status = order.Status
        };
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _repository.GetAllOrdersAsync();
        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            ProductIds = o.ProductIds,
            TotalAmount = o.TotalAmount,
            Status = o.Status
        });
    }

    public async Task<OrderDto?> UpdateOrderAsync(OrderDto dto)
    {
        var order = new Order
        {
            Id = dto.Id,
            UserId = dto.UserId,
            ProductIds = dto.ProductIds,
            TotalAmount = dto.TotalAmount,
            Status = dto.Status
        };

        var updated = await _repository.UpdateOrderAsync(order);
        if (updated == null) return null;

        return new OrderDto
        {
            Id = updated.Id,
            UserId = updated.UserId,
            ProductIds = updated.ProductIds,
            TotalAmount = updated.TotalAmount,
            Status = updated.Status
        };
    }

    public async Task<bool> DeleteOrderAsync(string id)
    {
        return await _repository.DeleteOrderAsync(id);
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
    {
        var orders = await _repository.GetOrdersByUserIdAsync(userId);
        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            ProductIds = o.ProductIds,
            TotalAmount = o.TotalAmount,
            Status = o.Status
        });
    }
    
    
}
