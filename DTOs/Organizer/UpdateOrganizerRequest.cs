using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Organizer;

public class UpdateOrganizerRequest
{
    [MaxLength(100, ErrorMessage = "Name of organizer cannot exceed 100 characters.")]
    public string? Name { get; set; }

    [MaxLength(254, ErrorMessage = "Email cannot exceed 254 characters.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public string? Email { get; set; }

    [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public string? PhoneNumber { get; set; }

    [MaxLength(500, ErrorMessage = "Bio cannot exceed 500 characters.")]
    public string? Bio { get; set; }

    public string? LogoUrl { get; set; }
}