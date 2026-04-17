using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ticketing_backend.Models.Events;

public class EventImage
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public required Guid EventId {get; set;}

    [ForeignKey(nameof(EventId))]
    public Event? Event {get; set;}

    public string? Image {get; set;}

    public required bool? IsPrimary {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}