namespace Ticketing_backend.DTOs.Permissions;

public class RolePermissionResponse
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public Guid PermissionId { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public string PermissionDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}