// DTOs/User/UpdateUserRequest.cs
using System.ComponentModel.DataAnnotations;

namespace Ticketing_backend.DTOs.User;

public class UpdateUserRequest
{
    [MaxLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
    public string? FirstName { get; set; }

    [MaxLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
    public string? LastName { get; set; }

    [MaxLength(254, ErrorMessage = "Email address cannot exceed 254 characters.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string? Email { get; set; }

    [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public string? PhoneNumber { get; set; }
}