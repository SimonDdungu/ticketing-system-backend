using Ticketing_backend.Models.Events;
namespace Ticketing_backend.Repositories.Interfaces;

public interface IEventStaffRepository : IRepository<EventStaff> 
{
    Task<EventStaff?> GetByUserIdAsync(Guid id);

    Task<IEnumerable<EventStaff>> GetByEventIdAsync(Guid id);

}