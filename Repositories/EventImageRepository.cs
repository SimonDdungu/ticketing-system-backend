// Repositories/EventImageRepository.cs
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Events;
using Ticketing_backend.Repositories.Interfaces;

namespace Ticketing_backend.Repositories;

public class EventImageRepository : Repository<EventImage>, IEventImageRepository
{
    public EventImageRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<EventImage>> GetByEventIdAsync(Guid eventId) =>
        await _dbSet.Where(i => i.EventId == eventId).ToListAsync();

    public async Task<EventImage?> GetPrimaryImageAsync(Guid eventId) =>
        await _dbSet.FirstOrDefaultAsync(i => i.EventId == eventId && i.IsPrimary == true);
}