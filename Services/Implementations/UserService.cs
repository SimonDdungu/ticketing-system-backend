using Microsoft.AspNetCore.Identity;
using Ticketing_backend.DTOs.User;
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

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var users = _userManager.Users.ToList();

        return users.Select(u => u.ToResponse());
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

    public async Task<UserResponse?> GetByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user?.ToResponse();
    }

    public async Task<IEnumerable<UserResponse>> GetByFirstNameAsync(string firstName)
{
    var users = _userManager.Users.Where(u => u.FirstName.Contains(firstName)).ToList();

    return users.Select(u => u.ToResponse());
}

    public async Task<IEnumerable<UserResponse>> GetByLastNameAsync(string lastName)
    {
        var users = _userManager.Users.Where(u => u.LastName.Contains(lastName)).ToList();

        return users.Select(u => u.ToResponse());
    }

    public async Task<UserResponse?> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        
        return user?.ToResponse();
    }

    public async Task<UserResponse?> GetByPublicIdAsync(string publicId)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.PublicId == publicId);
        
        return user?.ToResponse();
    }
}