using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Events;
using Ticketing_backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.Filters;
namespace Ticketing_backend.Repositories.Implementations;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(AppDbContext context) : base(context) { }

    public async Task<PaginatedResponse<Event>> GetFilteredAsync(EventFilter filter)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(o => o.Title.Contains(filter.Title));

        if (!string.IsNullOrWhiteSpace(filter.Venue))
            query = query.Where(o => o.Venue.Contains(filter.Venue));

        if (filter.Status.HasValue)
            query = query.Where(o => o.Status == filter.Status.Value);

        if (filter.Start.HasValue)
            query = query.Where(o => o.Start >= filter.Start.Value);

        if (filter.End.HasValue)
            query = query.Where(o => o.End <= filter.End.Value);

        if (!string.IsNullOrWhiteSpace(filter.SortBy) &&  typeof(Event).GetProperty(filter.SortBy) is not null)
        {
            query = filter.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => EF.Property<object>(e, filter.SortBy))
                : query.OrderBy(e => EF.Property<object>(e, filter.SortBy));
        }
        else
        {
            query = query.OrderByDescending(e => e.CreatedAt);
        }

        if (filter.IsDeleted.HasValue)
            query = query.Where(u => u.IsDeleted == filter.IsDeleted.Value);



        var totalCount = await query.CountAsync();

        var organizers = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PaginatedResponse<Event>
        {
            Data = organizers,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IEnumerable<Event>> GetByTitleAsync(string title) =>
        await _dbSet.Where(e => e.Title.Contains(title)).ToListAsync();

    public async Task<IEnumerable<Event>> GetByVenueAsync(string venue) =>
        await _dbSet.Where(e => e.Venue.Contains(venue)).ToListAsync();

    public async Task<IEnumerable<Event>> GetByOrganizerIdAsync(Guid id) =>
        await _dbSet.Where(e => e.OrganizerId == id).ToListAsync();

    public async Task<IEnumerable<Event>> GetByStatusAsync(EventStatus status) =>
        await _dbSet.Where(e => e.Status == status).ToListAsync();

    public async Task<IEnumerable<Event>> GetByStartDateAsync(DateTime start) =>
        await _dbSet.Where(e => e.Start >= start).ToListAsync();

    public async Task<IEnumerable<Event>> GetByEndDateAsync(DateTime end) =>
        await _dbSet.Where(e => e.End <= end).ToListAsync();

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