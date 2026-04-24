using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Event;

public class UpdateImageDetail
{
    [Required(ErrorMessage = "Event is required")]
    public required Guid EventId { get; set; }

    public bool IsPrimary { get; set; } = false;
}