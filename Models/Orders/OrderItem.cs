using System.ComponentModel.DataAnnotations.Schema;
namespace Ticketing_backend.Models.Orders;

using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Models.Tickets;

[Index(nameof(OrderId))]
public class OrderItem
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public required Guid TicketTypeId {get; set;}

    [ForeignKey(nameof(TicketTypeId))]
    public TicketType? TicketType {get; set;}
    

    public required Guid OrderId {get; set;}

    public Order? Order {get; set;}

    public required int Quantity { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal PriceAtPurchase { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = [];

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;

}