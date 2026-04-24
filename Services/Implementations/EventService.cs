using Ticketing_backend.DTOs.Event;
using Ticketing_backend.Mappings;
using Ticketing_backend.Models.Events;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventResponse?> GetByIdAsync(Guid id)
    {
        var e = await _eventRepository.GetByIdAsync(id);
        return e?.ToResponse();
    }

    public async Task<IEnumerable<EventResponse>> GetAllAsync()
    {
        var events = await _eventRepository.GetAllAsync();
        return events.Select(e => e.ToResponse());
    }

    public async Task<EventResponse> CreateAsync(CreateEventRequest request)
    {
        var e = request.ToModel();
        _eventRepository.Add(e);
        await _eventRepository.SaveAsync();
        return e.ToResponse();
    }

    public async Task<EventResponse> UpdateAsync(Guid id, UpdateEventRequest request)
    {
        var e = await _eventRepository.GetByIdAsync(id);
        if (e is null) throw new KeyNotFoundException($"Event with id {id} not found.");
        e.UpdateModel(request);
        e.UpdatedAt = DateTime.UtcNow;
        _eventRepository.Update(e);
        await _eventRepository.SaveAsync();
        return e.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var e = await _eventRepository.GetByIdAsync(id);
        if (e is null) throw new KeyNotFoundException($"Event with id {id} not found.");
        _eventRepository.Delete(e);
        await _eventRepository.SaveAsync();
    }

    public async Task<IEnumerable<EventResponse>> GetByTitleAsync(string title)
    {
        var events = await _eventRepository.GetByTitleAsync(title);
        return events.Select(e => e.ToResponse());
    }

    public async Task<IEnumerable<EventResponse>> GetByVenueAsync(string venue)
    {
        var events = await _eventRepository.GetByVenueAsync(venue);
        return events.Select(e => e.ToResponse());
    }

    public async Task<IEnumerable<EventResponse>> GetByOrganizerIdAsync(Guid organizerId)
    {
        var events = await _eventRepository.GetByOrganizerIdAsync(organizerId);
        return events.Select(e => e.ToResponse());
    }

    public async Task<IEnumerable<EventResponse>> GetByStatusAsync(EventStatus status)
    {
        var events = await _eventRepository.GetByStatusAsync(status);
        return events.Select(e => e.ToResponse());
    }

    public async Task<IEnumerable<EventResponse>> GetByStartDateAsync(DateTime start)
    {
        var events = await _eventRepository.GetByStartDateAsync(start);
        return events.Select(e => e.ToResponse());
    }

    public async Task<IEnumerable<EventResponse>> GetByEndDateAsync(DateTime end)
    {
        var events = await _eventRepository.GetByEndDateAsync(end);
        return events.Select(e => e.ToResponse());
    }

    public async Task<IEnumerable<EventResponse>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        var events = await _eventRepository.GetByDateRangeAsync(start, end);
        return events.Select(e => e.ToResponse());
    }

    public async Task<EventResponse?> GetWithImagesAsync(Guid id)
    {
        var e = await _eventRepository.GetWithImagesAsync(id);
        return e?.ToResponse();
    }

    public async Task<EventResponse?> GetWithTicketTypesAsync(Guid id)
    {
        var e = await _eventRepository.GetWithTicketTypesAsync(id);
        return e?.ToResponse();
    }

    public async Task<IEnumerable<EventResponse>> GetAllWithImagesAsync()
    {
        var events = await _eventRepository.GetAllWithImagesAsync();
        return events.Select(e => e.ToResponse());
    }
}