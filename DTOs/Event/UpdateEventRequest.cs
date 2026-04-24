using System.ComponentModel.DataAnnotations;
using Ticketing_backend.Models.Events;

namespace Ticketing_backend.DTOs.Event;

public class UpdateEventRequest
{
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string? Title { get; set; }

    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }

    [MaxLength(255, ErrorMessage = "Venue cannot exceed 255 characters.")]
    public string? Venue { get; set; }

    public double? Latitude { get; set; }
    
    public double? Longitude { get; set; }

    public string? PosterUrl {get; set;}

    public string? CoverUrl {get; set;}

    public EventStatus? Status { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }
}