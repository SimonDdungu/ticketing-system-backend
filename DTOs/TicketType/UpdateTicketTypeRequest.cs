using System.ComponentModel.DataAnnotations;
using Ticketing_backend.Constants;
namespace Ticketing_backend.DTOs.TicketType;

public class UpdateTicketTypeRequest
{
    [MaxLength(50, ErrorMessage = "Ticket name cannot exceed 50 characters")]
    public string? Name { get; set; }

    [Range(0, ValidationConstants.MaxPrice, ErrorMessage = "Invalid Price")]
    public decimal? Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Invalid Quantity.")]
    public int? QuantityAdded { get; set; }
}