
using Ticketing_backend.DTOs.Organizer;

namespace Ticketing_backend.Services.Interfaces;

public interface IOrganizerService : IService<OrganizerResponse, CreateOrganizerRequest, UpdateOrganizerRequest>
{
    Task<OrganizerResponse?> GetByUserIdAsync(Guid userId);
    Task<OrganizerResponse?> GetByEmailAsync(string email);
    Task<OrganizerResponse?> GetByPhoneNumberAsync(string phoneNumber);
    Task<OrganizerResponse?> GetWithEventsAsync(Guid id);
}