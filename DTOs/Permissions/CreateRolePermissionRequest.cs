using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Permissions;

public class CreateRolePermissionRequest
{
    [Required(ErrorMessage = "Role is required.")]
    public required Guid RoleId { get; set; }
    

    [Required(ErrorMessage = "Permission is required.")]
    public required Guid PermissionId { get; set; }
}