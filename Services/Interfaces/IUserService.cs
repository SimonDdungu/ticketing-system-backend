using Ticketing_backend.DTOs.Pagination;
using Ticketing_backend.DTOs.User;
using Ticketing_backend.Filters;

namespace Ticketing_backend.Services.Interfaces;

public interface IUserService
{
    Task<UserResponse?> GetByIdAsync(Guid id);

    Task<PaginatedResponse<UserResponse>> GetAllAsync(UserFilterRequest filter);

    Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request);

    Task DeleteAsync(Guid id);

}