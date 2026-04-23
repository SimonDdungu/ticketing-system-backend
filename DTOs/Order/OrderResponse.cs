namespace Ticketing_backend.DTOs.Order;

public class OrderResponse
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public string ReferenceNumber { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public PaymentStatus Status { get; set; }

    public string? TransactionId { get; set; }

    public List<OrderItemResponse> Items { get; set; } = [];

    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}