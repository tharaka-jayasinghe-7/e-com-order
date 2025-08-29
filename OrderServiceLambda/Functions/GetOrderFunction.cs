using System.Text.Json;
using Application.Services;
using Infrastructure.Repositories;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;


namespace OrderServiceLambda.Functions;

public class GetOrderFunction
{
    private readonly OrderService _service;

    public GetOrderFunction()
    {
        var dynamoDb = new AmazonDynamoDBClient();
        var repo = new DynamoDbOrderRepository(dynamoDb);
        _service = new OrderService(repo);
    }

    public async Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (request.PathParameters == null || !request.PathParameters.ContainsKey("id"))
            return new APIGatewayProxyResponse { StatusCode = 400, Body = "Order ID required" };

        var id = request.PathParameters["id"];
        var order = await _service.GetOrderAsync(id);

        if (order == null)
            return new APIGatewayProxyResponse { StatusCode = 404, Body = "Not Found" };

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonSerializer.Serialize(order),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
}