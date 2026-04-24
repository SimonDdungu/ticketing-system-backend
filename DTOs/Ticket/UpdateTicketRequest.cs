using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Ticket;
public class UpdateTicketRequest
{
    [Required(ErrorMessage = "QR Code is required to identify the ticket")]
    public required string QRCode { get; set; }

}