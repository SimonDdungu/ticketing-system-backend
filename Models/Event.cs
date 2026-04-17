using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ticketing_backend.Models;

public class Event
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public required Guid OrganizerId { get; set; }
    
    [ForeignKey(nameof(OrganizerId))]
    public Organizer? Organizer {get; set;}

    [MaxLength(200)]
    public required string Title {get; set;}

    [MaxLength(500)]
    public required string Description {get; set;}

    [MaxLength(255)]
    public required string Venue {get; set;}

    public double? Latitude {get; set;} 

    public double? Longitude {get; set;} 

    public required DateTime Start {get; set;}

    public required DateTime End {get; set;}


    public EventStatus Status {get; set;} = EventStatus.Draft;

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}