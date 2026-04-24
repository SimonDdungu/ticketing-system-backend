using Ticketing_backend.DTOs.Event;

namespace Ticketing_backend.Services.Interfaces;

public interface IEventImageService
{
    Task<IEnumerable<EventImageResponse>> GetByEventIdAsync(Guid eventId);
    Task<EventImageResponse?> GetPrimaryImageAsync(Guid eventId);
    Task<IEnumerable<EventImageResponse>> CreateAsync(CreateEventImageRequest request);
    Task<EventImageResponse> UpdateAsync(Guid imageId, UpdateImageDetail request);
    Task DeleteAsync(Guid imageId);
}