using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ticketing_backend.Models.Users;
namespace Ticketing_backend.Models.Organizers;

public class Organizer
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public required Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

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