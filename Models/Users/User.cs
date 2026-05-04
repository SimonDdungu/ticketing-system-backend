using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using Ticketing_backend.Models.Organizers;
using Ticketing_backend.Models.Permissions;

namespace Ticketing_backend.Models.Users;

[Index(nameof(PublicId))]
public class User : IdentityUser<Guid>
{
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [MaxLength(100)]
    public required string LastName { get; set; }

    public string PublicId { get; set; } = $"user-{Nanoid.Generate(Nanoid.Alphabets.UppercaseLettersAndDigits, 8)}";

    public ICollection<UserPermission> UserPermissions { get; set; } = [];

    public ICollection<Organizer> Organizers { get; set; } = [];

    public bool IsBanned { get; set; } = false;

    public string? BanReason { get; set; }

    public DateTime? BannedAt {get; set; }

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public Guid? UpdatedByUserId { get; set; }

    public Guid? BannedByUserId { get; set; }

    public Guid? DeletedByUserId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}