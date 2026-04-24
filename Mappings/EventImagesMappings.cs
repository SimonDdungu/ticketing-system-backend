using Ticketing_backend.DTOs.Event;
using Ticketing_backend.Models.Events;

namespace Ticketing_backend.Mappings;

public static class EventImageMappings
{
    public static EventImage ToModel(this ImageUploadDetail request, Guid eventId) => new()
    {
        EventId = eventId,
        ImageUrl = request.ImageUrl,
        IsPrimary = request.IsPrimary
    };

    public static EventImageResponse ToResponse(this EventImage image) => new()
    {
        Id = image.Id,
        ImageUrl = image.ImageUrl,
        IsPrimary = image.IsPrimary ?? false
    };

    public static void UpdateModel(this EventImage image, UpdateImageDetail request)
    {
        image.IsPrimary = request.IsPrimary;
    }
}