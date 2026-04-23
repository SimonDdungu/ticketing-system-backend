using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.User;

public class CreateUserRequest
{
    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Please provide an email address.")]
    [MaxLength(254, ErrorMessage = "Email cannot exceed 254 characters.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public required string Email { get; set; }

    [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public required string Password { get; set; }
}