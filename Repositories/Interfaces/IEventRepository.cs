using Ticketing_backend.Models.Events;
namespace Ticketing_backend.Repositories.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<IEnumerable<Event>> GetByTitleAsync(string title);

    Task<IEnumerable<Event>> GetByVenueAsync(string venue);

    Task<IEnumerable<Event>> GetByOrganizerIdAsync(Guid organizerId);

    Task<IEnumerable<Event>> GetByStatusAsync(EventStatus status);

    Task<IEnumerable<Event>> GetByDateRangeAsync(DateTime start, DateTime end);

    Task<IEnumerable<Event>> GetAllWithImagesAsync();

    Task<Event?> GetWithImagesAsync(Guid id);

    Task<Event?> GetWithTicketTypesAsync(Guid id);
}