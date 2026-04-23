using System.ComponentModel.DataAnnotations;
namespace Ticketing_backend.DTOs.Order;

public class UpdateOrderRequest
{
    public PaymentStatus? Status { get; set; }

    public string? TransactionId { get; set; }
}