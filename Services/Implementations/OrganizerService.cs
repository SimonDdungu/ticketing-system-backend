using Ticketing_backend.DTOs.Organizer;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.DTOs.SoftDelete;
using Ticketing_backend.Filters;
using Ticketing_backend.Mappings;
using Ticketing_backend.Middleware;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services.Implementations;

public class OrganizerService : IOrganizerService
{
    private readonly IOrganizerRepository _organizerRepository;

    private readonly UserContext _userContext;

    public OrganizerService(IOrganizerRepository organizerRepository, UserContext userContext)
    {
        _organizerRepository = organizerRepository;
        _userContext = userContext;
    }

    public async Task<OrganizerResponse?> GetByIdAsync(Guid id)
    {
        var organizer = await _organizerRepository.GetByIdAsync(id);

        return organizer?.ToResponse();
    }

    public async Task<PaginatedResponse<OrganizerResponse>> GetFilteredAsync(OrganizerFilter filter)
    {
        var result = await _organizerRepository.GetFilteredAsync(filter);

        return new PaginatedResponse<OrganizerResponse>
        {
            Data = result.Data.Select(o => o.ToResponse()),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<IEnumerable<OrganizerResponse>> GetAllAsync()
    {
        var organizers = await _organizerRepository.GetAllAsync();
        
        return organizers.Select(o => o.ToResponse());
    }

    public async Task<OrganizerResponse> CreateAsync(CreateOrganizerRequest request)
    {
        var UserId = _userContext.UserId;

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can create Organizers");
        }

        var organizer = request.ToModel();

        organizer.UserId = UserId.Value;
        
        _organizerRepository.Add(organizer);

        await _organizerRepository.SaveAsync();

        return organizer.ToResponse();
    }

    public async Task<OrganizerResponse> UpdateAsync(Guid id, UpdateOrganizerRequest request)
    {
        var organizer = await _organizerRepository.GetByIdAsync(id);
        var UserId = _userContext.UserId;

        if (organizer is null) throw new KeyNotFoundException($"Organizer with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can create Organizers");
        }

        if(organizer.UserId != UserId && !_userContext.IsStaff)
        {
            throw new ForbiddenAccessException("You are not allowed to update this Organizer.");
        }

        organizer.UpdateModel(request);
        organizer.UpdatedByUserId = UserId.Value;
        organizer.UpdatedAt = DateTime.UtcNow;

        _organizerRepository.Update(organizer);

        await _organizerRepository.SaveAsync();

        return organizer.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var organizer = await _organizerRepository.GetByIdAsync(id);
        var UserId = _userContext.UserId;

        if (organizer is null) throw new KeyNotFoundException($"Organizer with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can create Organizers");
        }

        if(organizer.UserId != UserId && !_userContext.IsAdmins)
        {
            throw new ForbiddenAccessException("You are not allowed to delete this Organizer.");
        }

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


    public async Task SoftDeleteAsync(Guid id, SoftDeleteRequest request)
    {
        var organizer = await _organizerRepository.GetByIdAsync(id);
        var UserId = _userContext.UserId;

        if(organizer is null) throw new KeyNotFoundException($"Organizer with id {id} can not be found");

        
        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can create Organizers");
        }

        if(organizer.UserId != UserId && !_userContext.IsAdmins)
        {
            throw new ForbiddenAccessException("You are not allowed to delete this Organizer.");
        }
        
        organizer.IsDeleted = request.IsDeleted;
        organizer.DeletedAt = request.IsDeleted ? DateTime.UtcNow : null;
        organizer.DeletedByUserId = request.IsDeleted ? UserId.Value : null;

        _organizerRepository.Update(organizer);

        await _organizerRepository.SaveAsync();
    }
}