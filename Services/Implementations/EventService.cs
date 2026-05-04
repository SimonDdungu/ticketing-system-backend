using Ticketing_backend.DTOs.Event;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.DTOs.SoftDelete;
using Ticketing_backend.Filters;
using Ticketing_backend.Mappings;
using Ticketing_backend.Middleware;
using Ticketing_backend.Models.Events;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    private readonly IOrganizerRepository _organizerRepository;

    private readonly UserContext _userContext;

    public EventService(IEventRepository eventRepository, IOrganizerRepository organizerRepository, UserContext userContext)
    {
        _eventRepository = eventRepository;
        _organizerRepository = organizerRepository;
        _userContext = userContext;
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

    public async Task<PaginatedResponse<EventResponse>> GetFilteredAsync(EventFilter filter)
    {
        var result = await _eventRepository.GetFilteredAsync(filter);

        return new PaginatedResponse<EventResponse>
        {
            Data = result.Data.Select(o => o.ToResponse()),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<EventResponse> CreateAsync(CreateEventRequest request)
    {
        var organizer = await _organizerRepository.GetByIdAsync(request.OrganizerId);
        var UserId = _userContext.UserId;

        if (organizer is null) 
            throw new KeyNotFoundException($"Organizer with id {request.OrganizerId} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can create Events");
        }

        if (organizer.UserId != UserId && !_userContext.IsStaff)
            throw new ForbiddenAccessException("You are not allowed to create an event for this organizer.");


        var e = request.ToModel();
        e.CreatedByUserId = UserId.Value;

        _eventRepository.Add(e);
        await _eventRepository.SaveAsync();
        return e.ToResponse();
    }

    public async Task<EventResponse> UpdateAsync(Guid id, UpdateEventRequest request)
    {
        var e = await _eventRepository.GetByIdAsync(id);
        var UserId = _userContext.UserId;

        if (e is null) throw new KeyNotFoundException($"Event with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can update Events");
        }

        if (e.Organizer.UserId != UserId && !_userContext.IsStaff)
            throw new ForbiddenAccessException("Only the Organizer for this event can make changes.");


        e.UpdateModel(request);
        e.UpdatedByUserId = UserId.Value;
        e.UpdatedAt = DateTime.UtcNow;

        _eventRepository.Update(e);
        await _eventRepository.SaveAsync();
        return e.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var e = await _eventRepository.GetByIdAsync(id);
        var UserId = _userContext.UserId;

        if (e is null) throw new KeyNotFoundException($"Event with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can delete Events");
        }


         if (e.Organizer.UserId != UserId && !_userContext.IsAdmins)
            throw new ForbiddenAccessException("Only the Organizer for this event can delete the event.");

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

    public async Task SoftDeleteAsync(Guid id, SoftDeleteRequest request)
    {
        var events = await _eventRepository.GetByIdAsync(id);
        var UserId = _userContext.UserId;

        if(events is null) throw new KeyNotFoundException($"Event with id {id} can not be found");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can delete Events");
        }


        if (events.Organizer.UserId != UserId && !_userContext.IsAdmins)
            throw new ForbiddenAccessException("Only the Organizer for this event can delete the event.");

        
        events.IsDeleted = request.IsDeleted;
        events.DeletedAt = request.IsDeleted ? DateTime.UtcNow : null;
        events.DeletedByUserId = request.IsDeleted ? UserId.Value : null;

        _eventRepository.Update(events);

        await _eventRepository.SaveAsync();
    }

}