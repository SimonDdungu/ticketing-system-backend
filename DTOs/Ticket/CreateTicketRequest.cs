using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Ticket;
public class CreateTicketRequest
{
    [Required(ErrorMessage = "Ticket must belong to an order item")]
    public required Guid OrderItemId { get; set; }

    [Required(ErrorMessage = "Ticket must have a ticket Type")]
    public required Guid TicketTypeId { get; set; }

    [Required(ErrorMessage = "Ticket must belong to an event")]
    public required Guid EventId { get; set; }
}