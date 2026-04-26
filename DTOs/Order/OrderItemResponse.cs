using Ticketing_backend.DTOs.Ticket;
namespace Ticketing_backend.DTOs.Order;

public class OrderItemResponse
{
    public Guid Id { get; set; }

    public Guid TicketTypeId { get; set; }

    public string TicketTypeName { get; set; } = string.Empty;

    public string EventName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal PriceAtPurchase { get; set; }

    public decimal SubTotal => PriceAtPurchase * Quantity;

    public List<TicketResponse> Tickets { get; set; } = [];

}