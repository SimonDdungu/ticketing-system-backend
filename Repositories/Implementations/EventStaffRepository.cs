using Ticketing_backend.Repositories.Interfaces;
namespace Ticketing_backend.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Events;

public class EventStaffRepository : Repository<EventStaff>, IEventStaffRepository
{
    public EventStaffRepository(AppDbContext context) : base(context) { }

    public async Task<EventStaff?> GetByUserIdAsync(Guid id) =>
        await _dbSet.FirstOrDefaultAsync(e => e.UserId == id);

    public async Task<IEnumerable<EventStaff>> GetByEventIdAsync(Guid id) =>
        await _dbSet.Where(e => e.EventId == id).ToListAsync();
}