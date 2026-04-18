namespace Ticketing_backend.DTOs.User;

public class UserResponse
{
    public Guid Id { get; set; }

    public string PublicId {get; set;} = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}