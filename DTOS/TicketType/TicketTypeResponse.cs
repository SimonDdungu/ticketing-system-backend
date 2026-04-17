namespace Ticketing_backend.DTOs.TicketType;

public class TicketTypeResponse
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int QuantityAvailable { get; set; }
    public int QuantityAdded { get; set; }
    public int QuantityRemaining { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}