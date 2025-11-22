namespace Domain.Entities;
using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Orders")]
public class Order
{
    [DynamoDBHashKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public List<string> ProductIds { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Customer details
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // Shipping / billing
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    // Payment
    public string PaymentMethod { get; set; } = string.Empty;
}