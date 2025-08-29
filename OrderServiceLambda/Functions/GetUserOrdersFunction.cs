using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Application.Services;
using Infrastructure.Repositories;

namespace OrderServiceLambda.Functions;

public class GetUserOrdersFunction
{
    private readonly OrderService _service;

    public GetUserOrdersFunction()
    {
        var dynamoDb = new Amazon.DynamoDBv2.AmazonDynamoDBClient();
        _service = new OrderService(new DynamoDbOrderRepository(dynamoDb));
    }

    public async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (!request.PathParameters.TryGetValue("userId", out var userId))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 400,
                Body = JsonSerializer.Serialize(new { message = "Missing 'userId' path parameter." }),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        var orders = await _service.GetOrdersByUserIdAsync(userId);

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonSerializer.Serialize(orders),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}