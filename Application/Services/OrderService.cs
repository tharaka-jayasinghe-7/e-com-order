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
            Status = string.IsNullOrEmpty(dto.Status) ? "Pending" : dto.Status,
            CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt,

            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,

            Address = dto.Address,
            City = dto.City,
            PostalCode = dto.PostalCode,

            PaymentMethod = dto.PaymentMethod
        };

        var created = await _repository.AddOrderAsync(order);

        return new OrderDto
        {
            Id = created.Id,
            UserId = created.UserId,
            ProductIds = created.ProductIds,
            TotalAmount = created.TotalAmount,
            Status = created.Status,
            CreatedAt = created.CreatedAt,

            FirstName = created.FirstName,
            LastName = created.LastName,
            Email = created.Email,
            PhoneNumber = created.PhoneNumber,

            Address = created.Address,
            City = created.City,
            PostalCode = created.PostalCode,

            PaymentMethod = created.PaymentMethod
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
            Status = order.Status,
            CreatedAt = order.CreatedAt,

            FirstName = order.FirstName,
            LastName = order.LastName,
            Email = order.Email,
            PhoneNumber = order.PhoneNumber,

            Address = order.Address,
            City = order.City,
            PostalCode = order.PostalCode,

            PaymentMethod = order.PaymentMethod
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
            Status = o.Status,
            CreatedAt = o.CreatedAt,

            FirstName = o.FirstName,
            LastName = o.LastName,
            Email = o.Email,
            PhoneNumber = o.PhoneNumber,

            Address = o.Address,
            City = o.City,
            PostalCode = o.PostalCode,

            PaymentMethod = o.PaymentMethod
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
            Status = dto.Status,
            CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt,

            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,

            Address = dto.Address,
            City = dto.City,
            PostalCode = dto.PostalCode,

            PaymentMethod = dto.PaymentMethod
        };

        var updated = await _repository.UpdateOrderAsync(order);
        if (updated == null) return null;

        return new OrderDto
        {
            Id = updated.Id,
            UserId = updated.UserId,
            ProductIds = updated.ProductIds,
            TotalAmount = updated.TotalAmount,
            Status = updated.Status,
            CreatedAt = updated.CreatedAt,

            FirstName = updated.FirstName,
            LastName = updated.LastName,
            Email = updated.Email,
            PhoneNumber = updated.PhoneNumber,

            Address = updated.Address,
            City = updated.City,
            PostalCode = updated.PostalCode,

            PaymentMethod = updated.PaymentMethod
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
            Status = o.Status,
            CreatedAt = o.CreatedAt,

            FirstName = o.FirstName,
            LastName = o.LastName,
            Email = o.Email,
            PhoneNumber = o.PhoneNumber,

            Address = o.Address,
            City = o.City,
            PostalCode = o.PostalCode,

            PaymentMethod = o.PaymentMethod
        });
    }
    
    
}
