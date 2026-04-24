using Ticketing_backend.DTOs.Ticket;
using Ticketing_backend.Models.Tickets;

namespace Ticketing_backend.Mappings;

public static class TicketMappings
{
    public static Ticket ToModel(this CreateTicketRequest request) => new()
    {
        TicketTypeId = request.TicketTypeId,
        EventId = request.EventId,
        OrderItemId = request.OrderItemId
    };

    public static TicketResponse ToResponse(this Ticket ticket) => new()
    {
        Id = ticket.Id,
        QRCode = ticket.QRCode,
        IsScanned = ticket.IsScanned,
        ScannedAt = ticket.ScannedAt
    };

    public static void UpdateModel(this Ticket ticket)
    {
        ticket.IsScanned = true;
        ticket.ScannedAt = DateTime.UtcNow;
        ticket.UpdatedAt = DateTime.UtcNow;
    }
}