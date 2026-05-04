using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.DTOs.SoftDelete;
using Ticketing_backend.DTOs.User;
using Ticketing_backend.Filters;
using Ticketing_backend.Mappings;
using Ticketing_backend.Middleware;
using Ticketing_backend.Models.Users;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    private readonly IUserRepository _userRepository;

    private readonly UserContext _userContext;

    public UserService(UserManager<User> userManager, IUserRepository userRepository, UserContext userContext)
    {
        _userManager = userManager;

        _userRepository = userRepository;

        _userContext = userContext;
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var UserId = _userContext.UserId;


        if (user is null) throw new KeyNotFoundException($"User with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can view this User");
        }

        if (user.Id != UserId && !_userContext.IsStaff)
            throw new ForbiddenAccessException("You are not allowed to view this User.");


        return user?.ToResponse();
    }

    public async Task<PaginatedResponse<UserResponse>> GetAllAsync(UserFilterRequest filter)
    {
        var UserId = _userContext.UserId;

        if(UserId is null || !_userContext.IsStaff)
        {
            throw new ForbiddenAccessException("You are not authorzied");
        }


        var result = await _userRepository.GetAllAsync(filter);
        
        return new PaginatedResponse<UserResponse>
        {
            Data = result.Data.Select(u => u.ToResponse()),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var UserId = _userContext.UserId;


        if (user is null) throw new KeyNotFoundException($"User with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can update User");
        }

        if (user.Id != UserId && !_userContext.IsStaff)
            throw new ForbiddenAccessException("You are not allowed to update this User.");



        if (request.FirstName is not null) user.FirstName = request.FirstName;

        if (request.LastName is not null) user.LastName = request.LastName;

        if (request.Email is not null) user.Email = request.Email;

        if (request.PhoneNumber is not null) user.PhoneNumber = request.PhoneNumber;

        user.UpdatedByUserId = UserId.Value;
        user.UpdatedAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return user.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var UserId = _userContext.UserId;

        if (user is null) throw new KeyNotFoundException($"User with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can update User");
        }

        if (user.Id != UserId && !_userContext.IsStaff)
            throw new ForbiddenAccessException("You are not allowed to delete this User.");


        await _userManager.DeleteAsync(user);
    }

    public async Task SoftDeleteAsync(Guid id, SoftDeleteRequest request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var UserId = _userContext.UserId;
        
        if (user is null) throw new KeyNotFoundException($"User with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can update User");
        }

        if (user.Id != UserId && !_userContext.IsStaff)
            throw new ForbiddenAccessException("You are not allowed to delete this User.");


        user.IsDeleted = request.IsDeleted;
        user.DeletedAt = request.IsDeleted ? DateTime.UtcNow : null;
        user.DeletedByUserId = request.IsDeleted ? UserId.Value : null;

        await _userManager.UpdateAsync(user);
    }

    public async Task BanAsync(Guid id, BanUserRequest request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var UserId = _userContext.UserId;
        
        if (user is null) throw new KeyNotFoundException($"User with id {id} not found.");

        if(UserId is null)
        {
            throw new UnauthorizedAccessException("Only authenticated users can update User");
        }

        if (user.Id != UserId && !_userContext.IsStaff)
            throw new ForbiddenAccessException("You are not allowed to delete this User.");


        user.IsBanned = request.IsBanned;
        user.BanReason = request.IsBanned ? request.Reason : null;
        user.BannedByUserId = request.IsBanned ? UserId.Value : null;
        user.BannedAt = request.IsBanned ? DateTime.UtcNow : null;

        await _userManager.UpdateAsync(user);
    }
}