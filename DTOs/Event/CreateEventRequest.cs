using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Event;

public class CreateEventRequest
{
    [Required(ErrorMessage = "Title for event is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "Description for event is required.")]
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public required string Description { get; set; }

    [Required(ErrorMessage = "Organizer for event is required.")]
    public required Guid OrganizerId { get; set; }

    [Required(ErrorMessage = "Venue for event is required.")]
    [MaxLength(255, ErrorMessage = "Venue cannot exceed 255 characters.")]
    public required string Venue { get; set; }

    public double? Latitude { get; set; }
    
    public double? Longitude { get; set; }

    public string? PosterUrl {get; set;}

    public string? CoverUrl {get; set;}

    [Required(ErrorMessage = "Start date and time for event is required.")]
    public required DateTime Start { get; set; }

    [Required(ErrorMessage = "End date and time for event is required.")]
    public required DateTime End { get; set; }
}