using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Users;
namespace Ticketing_backend.Models.Permissions;

[PrimaryKey(nameof(UserId), nameof(PermissionId))]
public class UserPermission
{
    public required Guid UserId { get; set; }
    public User? User { get; set; }

    public required Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}