using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Ticketing_backend.Models.Permissions;

[PrimaryKey(nameof(RoleId), nameof(PermissionId))]
public class RolePermission
{
    public required Guid RoleId { get; set; }
    public IdentityRole<Guid>? Role { get; set; }

    public required Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}