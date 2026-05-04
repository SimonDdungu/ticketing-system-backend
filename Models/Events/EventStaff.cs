using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Users;
namespace Ticketing_backend.Models.Events;

[Index(nameof(UserId))]
public class EventStaff
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required Guid UserId { get; set; }
    public User? User { get; set; }

    public required Guid EventId { get; set; }
    public Event? Event { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}