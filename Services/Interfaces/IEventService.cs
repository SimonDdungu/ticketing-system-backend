using Ticketing_backend.DTOs.Event;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.DTOs.SoftDelete;
using Ticketing_backend.Filters;
using Ticketing_backend.Models.Events;

namespace Ticketing_backend.Services.Interfaces;

public interface IEventService : IService<EventResponse, CreateEventRequest, UpdateEventRequest>
{
    Task<PaginatedResponse<EventResponse>> GetFilteredAsync(EventFilter filter);
    
    Task<IEnumerable<EventResponse>> GetByTitleAsync(string title);

    Task<IEnumerable<EventResponse>> GetByVenueAsync(string venue);

    Task<IEnumerable<EventResponse>> GetByOrganizerIdAsync(Guid organizerId);

    Task<IEnumerable<EventResponse>> GetByStatusAsync(EventStatus status);

    Task<IEnumerable<EventResponse>> GetByStartDateAsync(DateTime start);

    Task<IEnumerable<EventResponse>> GetByEndDateAsync(DateTime end);

    Task<IEnumerable<EventResponse>> GetByDateRangeAsync(DateTime start, DateTime end);

    Task<EventResponse?> GetWithImagesAsync(Guid id);

    Task<EventResponse?> GetWithTicketTypesAsync(Guid id);

    Task<IEnumerable<EventResponse>> GetAllWithImagesAsync();

    Task SoftDeleteAsync(Guid id,  SoftDeleteRequest request);

}