using Microsoft.AspNetCore.Identity;
namespace Ticketing_backend.Models.Permissions;

public class RolePermission
{
    public required Guid RoleId { get; set; }
    public IdentityRole<Guid> Role { get; set; } = null!;

    public required Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;
}