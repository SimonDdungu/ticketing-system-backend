namespace Ticketing_backend.DTOs.Permissions;

public class UserPermissionResponse
{
    public Guid UserId { get; set; }
    
    public string UserFullName { get; set; } = string.Empty;
    
    public Guid PermissionId { get; set; }
    
    public string PermissionName { get; set; } = string.Empty;
    
    public string PermissionDescription { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
}