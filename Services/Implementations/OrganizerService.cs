using Ticketing_backend.DTOs.Organizer;
using Ticketing_backend.Mappings;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services.Implementations;

public class OrganizerService : IOrganizerService
{
    private readonly IOrganizerRepository _organizerRepository;

    public OrganizerService(IOrganizerRepository organizerRepository)
    {
        _organizerRepository = organizerRepository;
    }

    public async Task<OrganizerResponse?> GetByIdAsync(Guid id)
    {
        var organizer = await _organizerRepository.GetByIdAsync(id);

        return organizer?.ToResponse();
    }

    public async Task<IEnumerable<OrganizerResponse>> GetAllAsync()
    {
        var organizers = await _organizerRepository.GetAllAsync();
        
        return organizers.Select(o => o.ToResponse());
    }

    public async Task<OrganizerResponse> CreateAsync(CreateOrganizerRequest request)
    {
        var organizer = request.ToModel(Guid.Empty); // replace with actual userId later after setting up Auth.
        
        _organizerRepository.Add(organizer);

        await _organizerRepository.SaveAsync();

        return organizer.ToResponse();
    }

    public async Task<OrganizerResponse> UpdateAsync(Guid id, UpdateOrganizerRequest request)
    {
        var organizer = await _organizerRepository.GetByIdAsync(id);

        if (organizer is null) throw new KeyNotFoundException($"Organizer with id {id} not found.");

        organizer.UpdateModel(request);

        organizer.UpdatedAt = DateTime.UtcNow;

        _organizerRepository.Update(organizer);

        await _organizerRepository.SaveAsync();

        return organizer.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var organizer = await _organizerRepository.GetByIdAsync(id);

        if (organizer is null) throw new KeyNotFoundException($"Organizer with id {id} not found.");

        _organizerRepository.Delete(organizer);

        await _organizerRepository.SaveAsync();
    }

    public async Task<OrganizerResponse?> GetByUserIdAsync(Guid userId)
    {
        var organizer = await _organizerRepository.GetByUserIdAsync(userId);

        return organizer?.ToResponse();
    }

    public async Task<OrganizerResponse?> GetByEmailAsync(string email)
    {
        var organizer = await _organizerRepository.GetByEmailAsync(email);

        return organizer?.ToResponse();
    }

    public async Task<OrganizerResponse?> GetByPhoneNumberAsync(string phoneNumber)
    {
        var organizer = await _organizerRepository.GetByPhoneNumberAsync(phoneNumber);

        return organizer?.ToResponse();
    }

    public async Task<OrganizerResponse?> GetWithEventsAsync(Guid id)
    {
        var organizer = await _organizerRepository.GetWithEventsAsync(id);

        return organizer?.ToResponse();
    }
}