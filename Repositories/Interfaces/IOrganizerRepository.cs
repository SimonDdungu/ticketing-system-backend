using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.Filters;
using Ticketing_backend.Models.Organizers;
namespace Ticketing_backend.Repositories.Interfaces;

public interface IOrganizerRepository : IRepository<Organizer>
{
    Task<Organizer?> GetByUserIdAsync(Guid userId);

    Task<PaginatedResponse<Organizer>> GetFilteredAsync(OrganizerFilter filter);
    
    Task<Organizer?> GetByEmailAsync(string email);
    
    Task<Organizer?> GetByPhoneNumberAsync(string phoneNumber);
    
    Task<Organizer?> GetWithEventsAsync(Guid id);
}