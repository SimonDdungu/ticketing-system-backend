using Ticketing_backend.DTOs.User;
using Ticketing_backend.Models.Users;

namespace Ticketing_backend.Mappings;

public static class UserMappings
{
    public static UserResponse ToResponse(this User user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email!,
        PhoneNumber = user.PhoneNumber,
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt
    };
}