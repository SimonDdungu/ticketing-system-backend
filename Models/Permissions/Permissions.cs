using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.Models.Permissions;

public class Permission
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)]
    public required string Name { get; set; } // e.g. "events.create"

    [MaxLength(255)]
    public required string Description { get; set; } // e.g. "Can create events"

    public ICollection<RolePermission> RolePermissions { get; set; } = [];

    public ICollection<UserPermission> UserPermissions { get; set; } = [];
}