using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Order;

public class CreateOrderItemRequest
{
    [Required(ErrorMessage = "Ticket type is required.")]
    public required Guid TicketTypeId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Quantity")]
    public required int Quantity { get; set; }
}