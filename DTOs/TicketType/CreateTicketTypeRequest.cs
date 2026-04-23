using System.ComponentModel.DataAnnotations;
using Ticketing_backend.Constants;
namespace Ticketing_backend.DTOs.TicketType;

public class CreateTicketTypeRequest
{
    [Required(ErrorMessage = "Event for ticket is required.")]
    public required Guid EventId {get; set;}

    [Required(ErrorMessage = "Please provide a name for the ticket.")]
    [MaxLength(50, ErrorMessage = "Ticket name cannot exceed 50 characters")]
    public required string Name {get; set;}

    [Required(ErrorMessage = "Please provide the price of the ticket.")]
    [Range(0, ValidationConstants.MaxPrice, ErrorMessage = "Invalid Price.")]
    public required decimal Price {get; set;}

    [Required(ErrorMessage = "Please provide how many tickets are available.")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid Quantity.")]
    public required int QuantityAvailable {get; set;}

}