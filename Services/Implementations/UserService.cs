using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.DTOs.User;
using Ticketing_backend.Filters;
using Ticketing_backend.Mappings;
using Ticketing_backend.Models.Users;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        return user?.ToResponse();
    }

    public async Task<PaginatedResponse<UserResponse>> GetAllAsync(UserFilterRequest filter)
    {
        var query = _userManager.Users.AsQueryable();

        if (filter.FirstName is not null)
            query = query.Where(u => u.FirstName.Contains(filter.FirstName));

        if (filter.LastName is not null)
            query = query.Where(u => u.LastName.Contains(filter.LastName));

        if (filter.Email is not null)
            query = query.Where(u => u.Email!.Contains(filter.Email));

        if (filter.PhoneNumber is not null)
            query = query.Where(u => u.PhoneNumber != null && u.PhoneNumber.Contains(filter.PhoneNumber));

        if (filter.PublicId is not null)
            query = query.Where(u => u.PublicId == filter.PublicId);

        var totalCount = await query.CountAsync();

        var users = await query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

        return new PaginatedResponse<UserResponse>
        {
            Data = users.Select(u => u.ToResponse()),
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null) throw new KeyNotFoundException($"User with id {id} not found.");

        

        if (request.FirstName is not null) user.FirstName = request.FirstName;

        if (request.LastName is not null) user.LastName = request.LastName;

        if (request.Email is not null) user.Email = request.Email;

        if (request.PhoneNumber is not null) user.PhoneNumber = request.PhoneNumber;

        user.UpdatedAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return user.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is null) throw new KeyNotFoundException($"User with id {id} not found.");

        await _userManager.DeleteAsync(user);
    }
}