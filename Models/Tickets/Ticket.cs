using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Orders;
namespace Ticketing_backend.Models.Tickets;

[Index(nameof(QRCode), IsUnique = true)]
public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required Guid OrderItemId { get; set; }
    public OrderItem? OrderItem { get; set; }

    [MaxLength(100)]
    public string QRCode { get; set; } = Guid.NewGuid().ToString();

    public bool IsScanned { get; set; } = false;
    public DateTime? ScannedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}