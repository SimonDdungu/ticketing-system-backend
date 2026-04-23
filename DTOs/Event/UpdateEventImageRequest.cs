using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Event;

public class UpdateEventImageRequest
{
    [Required(ErrorMessage = "Event is required")]
    public required Guid EventId { get; set; }

    public List<UpdateImageDetail> Images { get; set; } = [];
}

public class UpdateImageDetail
{
    // If Id is provided, it's an existing image in the DB.
    // If Id is null, it's a new image to be inserted.
    public Guid? Id { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsPrimary { get; set; } = false;
}