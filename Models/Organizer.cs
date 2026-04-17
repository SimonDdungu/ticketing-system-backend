using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.Models;

public class Organizer
{
    public Guid Id {get; set;} = Guid.NewGuid();

    [MaxLength(100)]
    public required string Name {get; set; }

    [EmailAddress]
    public required string Email {get; set;} 

    [MaxLength(15)]
    public string? PhoneNumber {get; set;}

    [MaxLength(500)]
    public string? Bio {get; set;}

    public string? LogoUrl {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;

}