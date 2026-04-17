using System.ComponentModel.DataAnnotations;

namespace Ticketing_backend.DTOs.User;

public class ChangePasswordRequest
{
    [Required(ErrorMessage = "Current password is required.")]
    public required string CurrentPassword { get; set; }

    [Required(ErrorMessage = "New password is required.")]
    [MinLength(8, ErrorMessage = "New Password must be at least 8 characters.")]
    public required string NewPassword { get; set; }

    [Required(ErrorMessage = "Please confirm your new password.")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; }
}