using Ticketing_backend.Models.Events;
namespace Ticketing_backend.DTOs.Event;

public class EventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid OrganizerId { get; set; }

    public string OrganizerName { get; set; } = string.Empty;

    public string Venue { get; set; } = string.Empty;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string PosterUrl {get; set;} = string.Empty;

    public string CoverUrl {get; set;} = string.Empty;

    public EventStatus Status {get; set;}

    public List<EventImageResponse> Images { get; set; } = [];

    public DateTime Start { get; set; }
    
    public DateTime End { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}