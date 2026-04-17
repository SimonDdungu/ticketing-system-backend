using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Ticketing_backend.Models.Users;

public class User : IdentityUser<Guid>
{
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [MaxLength(100)]
    public required string LastName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}