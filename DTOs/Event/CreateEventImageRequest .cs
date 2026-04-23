using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Event;

public class CreateEventImageRequest
{
    [Required(ErrorMessage = "Event is required")]
    public required Guid EventId { get; set; }

    // This allows  user to send may images at once e.g 10 images 
    public required List<ImageUploadDetail> Images { get; set; }
}

public class ImageUploadDetail
{
    [Required(ErrorMessage = "Image is required")]
    public required string ImageUrl { get; set; }

    public bool IsPrimary { get; set; } = false;
}