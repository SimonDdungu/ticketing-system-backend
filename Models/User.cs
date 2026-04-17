using System.ComponentModel.DataAnnotations;

namespace Ticketing_backend.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please provide a name.")]
    [MaxLength(100,  ErrorMessage = "Name cannot exceed 100 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Please provide an email address.")]
    [EmailAddress(ErrorMessage = "Please provide a valid email address.")]
    public required string Email { get; set; }

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}