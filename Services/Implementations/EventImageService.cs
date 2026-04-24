// Services/EventImageService.cs
using Ticketing_backend.DTOs.Event;
using Ticketing_backend.Mappings;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services;

public class EventImageService : IEventImageService
{
    private readonly IEventImageRepository _eventImageRepository;

    public EventImageService(IEventImageRepository eventImageRepository)
    {
        _eventImageRepository = eventImageRepository;
    }

    public async Task<IEnumerable<EventImageResponse>> GetByEventIdAsync(Guid eventId)
    {
        var images = await _eventImageRepository.GetByEventIdAsync(eventId);
        
        return images.Select(i => i.ToResponse());
    }

    public async Task<EventImageResponse?> GetPrimaryImageAsync(Guid eventId)
    {
        var image = await _eventImageRepository.GetPrimaryImageAsync(eventId);

        return image?.ToResponse();
    }

    public async Task<IEnumerable<EventImageResponse>> CreateAsync(CreateEventImageRequest request)
    {
        // ensure only one image is primary
        var hasPrimary = request.Images.Count(i => i.IsPrimary) > 1;

        if (hasPrimary) throw new InvalidOperationException("Only one image can be primary.");

        var images = request.Images.Select(i => i.ToModel(request.EventId)).ToList();

        foreach (var image in images)
            _eventImageRepository.Add(image);

        await _eventImageRepository.SaveAsync();

        return images.Select(i => i.ToResponse());
    }

    public async Task<EventImageResponse> UpdateAsync(Guid imageId, UpdateImageDetail request)
    {
        var image = await _eventImageRepository.GetByIdAsync(imageId);
        if (image is null) throw new KeyNotFoundException($"Image with id {imageId} not found.");

        // if user updates this image as the new primary, remove previous primary => Makes sure only 1 Image is Primary.
        if (request.IsPrimary)
        {
            var currentPrimary = await _eventImageRepository.GetPrimaryImageAsync(image.EventId);
            if (currentPrimary is not null && currentPrimary.Id != imageId)
            {
                currentPrimary.IsPrimary = false;
                _eventImageRepository.Update(currentPrimary);
            }
        }

        image.UpdateModel(request);

        image.UpdatedAt = DateTime.UtcNow;

        _eventImageRepository.Update(image);

        await _eventImageRepository.SaveAsync();

        return image.ToResponse();
    }

    public async Task DeleteAsync(Guid imageId)
    {
        var image = await _eventImageRepository.GetByIdAsync(imageId);

        if (image is null) throw new KeyNotFoundException($"Image with id {imageId} not found.");

        _eventImageRepository.Delete(image);

        await _eventImageRepository.SaveAsync();
    }
}