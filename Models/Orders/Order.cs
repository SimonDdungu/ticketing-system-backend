
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NanoidDotNet;
using Ticketing_backend.Models.Users;
namespace Ticketing_backend.Models.Orders;

[Index(nameof(ReferenceNumber), IsUnique = true)]
public class Order
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public string ReferenceNumber { get; set; } = $"ord-{Nanoid.Generate(Nanoid.Alphabets.UppercaseLettersAndDigits, 8)}";

    public required Guid UserId {get; set;}

    public User? User {get; set;}

    [Column(TypeName = "decimal(18, 2)")]
    public required decimal TotalAmount {get; set;}

    public PaymentStatus Status {get; set;} = PaymentStatus.Pending;

    public string? TransactionId {get; set;} 

    public ICollection<OrderItem> OrderItems {get; set;} = [];

    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}