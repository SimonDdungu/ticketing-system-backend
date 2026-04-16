using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.Models;

public class Organizer
{
    public Guid Id {get; set;} = Guid.NewGuid();

    [Required]
    public string Name {get; set; } = string.Empty;

    [Required]
    public string Email {get; set;} = string.Empty;

    public string? PhoneNumber {get; set;}

    public string? Bio {get; set;}

    public string? Logo_url {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}