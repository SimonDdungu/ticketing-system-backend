using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Ticket;
public class CreateTicketRequest
{
    [Required(ErrorMessage = "Ticket must belong to an order item")]
    public required Guid OrderItemId { get; set; }
}