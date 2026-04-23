using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Permissions;

public class CreateUserPermissionRequest
{
    [Required(ErrorMessage = "User is required.")]
    public required Guid UserId { get; set; }

    [Required(ErrorMessage = "Permission is required.")]
    public required Guid PermissionId { get; set; }
}