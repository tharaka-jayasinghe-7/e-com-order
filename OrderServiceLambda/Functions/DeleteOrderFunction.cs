using Application.Services;
using Infrastructure.Repositories;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;



namespace OrderServiceLambda.Functions;

public class DeleteOrderFunction
{
    private readonly OrderService _service;

    public DeleteOrderFunction()
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
        var deleted = await _service.DeleteOrderAsync(id);

        if (!deleted)
            return new APIGatewayProxyResponse { StatusCode = 404, Body = "Not Found" };

        return new APIGatewayProxyResponse { StatusCode = 204 };
    }
}