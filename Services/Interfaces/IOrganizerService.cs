using Ticketing_backend.DTOs.Organizer;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.Filters;

namespace Ticketing_backend.Services.Interfaces;

public interface IOrganizerService : IService<OrganizerResponse, CreateOrganizerRequest, UpdateOrganizerRequest>
{

    Task<OrganizerResponse?> GetByUserIdAsync(Guid userId);

    Task<bool> IsOwnerAsync(Guid organizerId, Guid userId);
    

    Task<PaginatedResponse<OrganizerResponse>> GetFilteredAsync(OrganizerFilter filter);
    
    Task<OrganizerResponse?> GetByEmailAsync(string email);

    Task<OrganizerResponse?> GetByPhoneNumberAsync(string phoneNumber);

    Task<OrganizerResponse?> GetWithEventsAsync(Guid id);
}