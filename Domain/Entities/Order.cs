namespace Domain.Entities;
using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Orders")]
public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public List<string> ProductIds { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}