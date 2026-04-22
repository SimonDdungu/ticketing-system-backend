using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Events;
using Ticketing_backend.Repositories.Interfaces;
namespace Ticketing_backend.Repositories;

public class EventRepository(AppDbContext context) : Repository<Event>(context), IEventRepository
{
    public async Task<IEnumerable<Event>> GetByTitleAsync(string title) =>
        await _dbSet.Where(e => e.Title.Contains(title)).ToListAsync();

    public async Task<IEnumerable<Event>> GetByVenueAsync(string venue) =>
        await _dbSet.Where(e => e.Venue.Contains(venue)).ToListAsync();

    public async Task<IEnumerable<Event>> GetByOrganizerIdAsync(Guid id) =>
        await _dbSet.Where(e => e.OrganizerId == id).ToListAsync();

    public async Task<IEnumerable<Event>> GetByStatusAsync(EventStatus status) =>
        await _dbSet.Where(e => e.Status == status).ToListAsync();

    public async Task<IEnumerable<Event>> GetByDateRangeAsync(DateTime start, DateTime end) =>
        await _dbSet.Where(e => e.Start >= start && e.End <= end).ToListAsync();

    public async Task<IEnumerable<Event>> GetAllWithImagesAsync() =>
        await _dbSet
            .Include(e => e.Images)
            .ToListAsync();

     public async Task<Event?> GetWithImagesAsync(Guid id) =>
        await _dbSet
            .Include(e => e.Images)
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<Event?> GetWithTicketTypesAsync(Guid id) =>
        await _dbSet
            .Include(e => e.TicketTypes)
            .FirstOrDefaultAsync(e => e.Id == id);
}