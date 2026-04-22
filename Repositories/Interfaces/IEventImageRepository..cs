using Ticketing_backend.Models.Events;

namespace Ticketing_backend.Repositories.Interfaces;

public interface IEventImageRepository : IRepository<EventImage>
{
    Task<IEnumerable<EventImage>> GetByEventIdAsync(Guid eventId);
    
    Task<EventImage?> GetPrimaryImageAsync(Guid eventId);
}