using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.Filters;
using Ticketing_backend.Models.Organizers;
using Ticketing_backend.Repositories.Interfaces;
namespace Ticketing_backend.Repositories.Implementations;

public class OrganizerRepository : Repository<Organizer>, IOrganizerRepository
{
    public OrganizerRepository(AppDbContext context) : base(context) { }

    public async Task<PaginatedResponse<Organizer>> GetFilteredAsync(OrganizerFilter filter)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(o => o.Name.Contains(filter.Name));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(o => o.Email.Contains(filter.Email));

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            query = query.Where(o => o.PhoneNumber != null && o.PhoneNumber.Contains(filter.PhoneNumber));

        if (!string.IsNullOrWhiteSpace(filter.SortBy) &&  typeof(Organizer).GetProperty(filter.SortBy) is not null)
        {
            query = filter.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => EF.Property<object>(e, filter.SortBy))
                : query.OrderBy(e => EF.Property<object>(e, filter.SortBy));
        }
        else
        {
            query = query.OrderByDescending(e => e.CreatedAt); // default sort
        }

        if (filter.IsDeleted.HasValue)
            query = query.Where(u => u.IsDeleted == filter.IsDeleted.Value);



        var totalCount = await query.CountAsync();

        var organizers = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return new PaginatedResponse<Organizer>
        {
            Data = organizers,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<Organizer?> GetByUserIdAsync(Guid userId) =>
        await _dbSet.FirstOrDefaultAsync(o => o.UserId == userId);

    public async Task<Organizer?> GetByEmailAsync(string email) =>
        await _dbSet.FirstOrDefaultAsync(o => o.Email == email);

    public async Task<Organizer?> GetByPhoneNumberAsync(string phoneNumber) =>
        await _dbSet.FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber);

    public async Task<Organizer?> GetWithEventsAsync(Guid id) =>
        await _dbSet.Include(o => o.Events).FirstOrDefaultAsync(o => o.Id == id);
}