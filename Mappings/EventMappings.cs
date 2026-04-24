using Ticketing_backend.DTOs.Event;
using Ticketing_backend.Models.Events;

namespace Ticketing_backend.Mappings;

public static class EventMappings
{
    public static EventResponse ToResponse(this Event e) => new()
    {
        Id = e.Id,
        OrganizerId = e.OrganizerId,
        OrganizerName = e.Organizer?.Name ?? string.Empty,
        Title = e.Title,
        Description = e.Description,
        Venue = e.Venue,
        Latitude = e.Latitude,
        Longitude = e.Longitude,
        PosterUrl = e.PosterUrl  ?? string.Empty,
        CoverUrl = e.CoverUrl  ?? string.Empty,
        Start = e.Start,
        End = e.End,
        Status = e.Status,
        Images = e.Images?.Select(i => i.ToResponse()).ToList() ?? [],
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public static Event ToModel(this CreateEventRequest request) => new()
    {
        OrganizerId = request.OrganizerId,
        Title = request.Title,
        Description = request.Description,
        Venue = request.Venue,
        Latitude = request.Latitude,
        Longitude = request.Longitude,
        PosterUrl = request.PosterUrl,
        Start = request.Start,
        End = request.End,
        Status = request.Status
    };

    public static void UpdateModel(this Event e, UpdateEventRequest request)
    {
        if (request.Title is not null) e.Title = request.Title;
        if (request.Description is not null) e.Description = request.Description;
        if (request.Venue is not null) e.Venue = request.Venue;
        if (request.Latitude is not null) e.Latitude = request.Latitude;
        if (request.Longitude is not null) e.Longitude = request.Longitude;
        if (request.PosterUrl is not null) e.PosterUrl = request.PosterUrl;
        if (request.Start is not null) e.Start = request.Start.Value;
        if (request.End is not null) e.End = request.End.Value;
        if (request.Status is not null) e.Status = request.Status.Value;
    }
}