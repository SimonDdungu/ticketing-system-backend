using Ticketing_backend.DTOs.TicketType;
using Ticketing_backend.Models.Tickets;

namespace Ticketing_backend.Mappings;

public static class TicketTypeMappings
{
    public static TicketTypeResponse ToResponse(this TicketType ticketType) => new()
    {
        Id = ticketType.Id,
        EventId = ticketType.EventId,
        Name = ticketType.Name,
        Price = ticketType.Price,
        QuantityAvailable = ticketType.QuantityAvailable,
        QuantityRemaining = ticketType.QuantityRemaining,
        CreatedAt = ticketType.CreatedAt,
        UpdatedAt = ticketType.UpdatedAt
    };

    public static TicketType ToModel(this CreateTicketTypeRequest request) => new()
    {
        EventId = request.EventId,
        Name = request.Name,
        Price = request.Price,
        QuantityAvailable = request.QuantityAvailable,
        QuantityRemaining = request.QuantityAvailable 
    };

    public static void UpdateModel(this TicketType ticketType, UpdateTicketTypeRequest request)
    {
        if (request.Name is not null) ticketType.Name = request.Name;
        if (request.Price is not null) ticketType.Price = request.Price.Value;

        if (request.QuantityAvailable is not null)
            {
                var sold = ticketType.QuantityAvailable - ticketType.QuantityRemaining;
                ticketType.QuantityAvailable = request.QuantityAvailable.Value;
                ticketType.QuantityRemaining = Math.Max(0, request.QuantityAvailable.Value - sold); //Prevents Negative
            }
    }
}