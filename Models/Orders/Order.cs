
using System.ComponentModel.DataAnnotations.Schema;
using Ticketing_backend.Models.Users;
namespace Ticketing_backend.Models.Orders;

public class Order
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public required Guid UserId {get; set;}

    public User? User {get; set;}

    [Column(TypeName = "decimal(18, 2)")]
    public required decimal TotalAmount {get; set;}

    public PaymentStatus Status {get; set;} = PaymentStatus.Pending;

    public string? TransactionID {get; set;} 

    public ICollection<OrderItem> Items {get; set;} = [];

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}