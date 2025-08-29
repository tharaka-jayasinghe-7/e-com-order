using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class DynamoDbOrderRepository : IOrderRepository
{
    private readonly DynamoDBContext _context;

    public DynamoDbOrderRepository(IAmazonDynamoDB dynamoDb)
    {
        _context = new DynamoDBContext(dynamoDb);
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        await _context.SaveAsync(order);
        return order;
    }

    public async Task<Order?> GetOrderByIdAsync(string id)
    {
        return await _context.LoadAsync<Order>(id);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        var conditions = new List<ScanCondition>();
        return await _context.ScanAsync<Order>(conditions).GetRemainingAsync();
    }

    public async Task<Order?> UpdateOrderAsync(Order order)
    {
        await _context.SaveAsync(order);
        return order;
    }

    public async Task<bool> DeleteOrderAsync(string id)
    {
        var order = await _context.LoadAsync<Order>(id);
        if (order == null) return false;

        await _context.DeleteAsync(order);
        return true;
    }
    
    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
    {
        // Using Scan with a condition
        var search = _context.ScanAsync<Order>(new List<ScanCondition>
        {
            new ScanCondition("UserId", ScanOperator.Equal, userId)
        });

        return await search.GetRemainingAsync();
    }
}