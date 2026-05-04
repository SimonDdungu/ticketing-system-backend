using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Organizers;
using Ticketing_backend.Models.Tickets;
using Ticketing_backend.Models.Users;
namespace Ticketing_backend.Models.Events;

[Index(nameof(OrganizerId))]

public class Event
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public required Guid OrganizerId { get; set; }
    
    [ForeignKey(nameof(OrganizerId))]
    public Organizer Organizer { get; set; } = null!;

    public required Guid CreatedByUserId { get; set; }

    public Guid? UpdatedByUserId { get; set; }

    public Guid? DeletedByUserId { get; set; }

    [MaxLength(200)]
    public required string Title {get; set;}

    [MaxLength(500)]
    public required string Description {get; set;}

    [MaxLength(255)]
    public required string Venue {get; set;}

    public double? Latitude {get; set;} 

    public double? Longitude {get; set;} 

    public string? PosterUrl {get; set;}

    public string? CoverUrl {get; set;}

    public required DateTime Start {get; set;}

    public required DateTime End {get; set;}


    public EventStatus Status {get; set;} = EventStatus.Draft;

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public ICollection<EventImage> Images { get; set; } = [];

    public ICollection<TicketType> TicketTypes { get; set; } = [];

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}