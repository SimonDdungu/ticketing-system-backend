using System.ComponentModel.DataAnnotations;

namespace Ticketing_backend.DTOs.Organizer;

public class CreateOrganizerRequest
{
    [Required(ErrorMessage = "Name of organizer is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Email of organizer is required.")]
    [MaxLength(254, ErrorMessage = "Email cannot exceed 254 characters.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public required string Email { get; set; }

    [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public string? PhoneNumber { get; set; }

    [MaxLength(500, ErrorMessage = "Bio cannot exceed 500 characters.")]
    public string? Bio { get; set; }

    public string? LogoUrl { get; set; }

    public Guid? UserId { get; set; }
}