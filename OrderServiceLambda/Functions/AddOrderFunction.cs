using System.Text.Json;
using Application.DTOs;
using Application.Services;
using Infrastructure.Repositories;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;



namespace OrderServiceLambda.Functions;

public class AddOrderFunction
{
    private readonly OrderService _service;

    public AddOrderFunction()
    {
        var dynamoDb = new AmazonDynamoDBClient();
        var repo = new DynamoDbOrderRepository(dynamoDb);
        _service = new OrderService(repo);
    }

    public async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var orderDto = JsonSerializer.Deserialize<OrderDto>(request.Body);
        var created = await _service.CreateOrderAsync(orderDto!);

        return new APIGatewayProxyResponse
        {
            StatusCode = 201,
            Body = JsonSerializer.Serialize(created),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}