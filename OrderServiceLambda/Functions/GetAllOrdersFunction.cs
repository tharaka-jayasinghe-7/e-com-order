using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Application.Services; // Your service namespace

namespace OrderServiceLambda.Functions;

public class GetAllOrdersFunction
{
    private readonly OrderService _service;

    public GetAllOrdersFunction()
    {
        var dynamoDb = new AmazonDynamoDBClient();
        var repo = new Infrastructure.Repositories.DynamoDbOrderRepository(dynamoDb);
        _service = new OrderService(repo);
    }

    public async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var orders = await _service.GetAllOrdersAsync(); // Implement this in your service

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonSerializer.Serialize(orders),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}