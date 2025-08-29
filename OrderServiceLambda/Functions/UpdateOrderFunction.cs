using System.Text.Json;
using Application.DTOs;
using Application.Services;
using Infrastructure.Repositories;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;



namespace OrderServiceLambda.Functions;

public class UpdateOrderFunction
{
    private readonly OrderService _service;

    public UpdateOrderFunction()
    {
        var dynamoDb = new AmazonDynamoDBClient();
        var repo = new DynamoDbOrderRepository(dynamoDb);
        _service = new OrderService(repo);
    }

    public async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var orderDto = JsonSerializer.Deserialize<OrderDto>(request.Body);
        var updated = await _service.UpdateOrderAsync(orderDto!);

        if (updated == null)
            return new APIGatewayProxyResponse { StatusCode = 404, Body = "Not Found" };

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonSerializer.Serialize(updated),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}