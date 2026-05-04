using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Events;
namespace Ticketing_backend.Models.Tickets;

[Index(nameof(EventId))]
public class TicketType
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public required Guid EventId {get; set;}

    [ForeignKey(nameof(EventId))]
    public Event? Event {get; set;}

    [MaxLength(50)]
    public required string Name {get; set;}

    [Column(TypeName = "decimal(18, 2)")]
    public required decimal Price {get; set;}

    public required int QuantityAvailable {get; set;}

    public int QuantityAdded { get; set; }

    public int QuantityRemaining {get; set;}

    public bool IsDeleted { get; set; } = false;

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}