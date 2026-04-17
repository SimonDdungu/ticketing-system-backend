using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Ticket;
public class UpdateTicketRequest
{
    [Required(ErrorMessage = "QR Code is required to identify the ticket")]
    public required string QRCode { get; set; }

    [Required(ErrorMessage = "The current Event is required for verification")]
    public required Guid CurrentEventId { get; set; }
}