namespace Application.DTOs;

public class OrderDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public List<string> ProductIds { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
}