using Ticketing_backend.DTOs.User;

namespace Ticketing_backend.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse?> GetByIdAsync(Guid id);

    Task<IEnumerable<UserResponse>> GetAllAsync();

    Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request);

    Task DeleteAsync(Guid id);

    Task<UserResponse?> GetByEmailAsync(string email);

    Task<IEnumerable<UserResponse>> GetByFirstNameAsync(string firstName);

    Task<IEnumerable<UserResponse>> GetByLastNameAsync(string lastName);
    
    Task<UserResponse?> GetByPhoneNumberAsync(string phoneNumber);
}