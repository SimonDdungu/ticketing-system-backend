using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Ticket;
public class TicketResponse
{
    public Guid Id { get; set; }

    public Guid TicketTypeId { get; set; } 
    public string TicketTypeName { get; set; } = string.Empty;

    public Guid EventId { get; set; }
    public string EventTitle { get; set; } = string.Empty;

    public Guid OrderItemId { get; set; }
    public decimal Price { get; set; }

    public string QRCode { get; set; } = string.Empty;

    public bool IsScanned { get; set; }

    public string OwnerName { get; set; } = string.Empty;
    
    public string OwnerEmail { get; set; } = string.Empty;

    public DateTime? ScannedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt {get; set;}
}