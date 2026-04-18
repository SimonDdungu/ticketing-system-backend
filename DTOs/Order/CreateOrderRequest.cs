using System.ComponentModel.DataAnnotations;

namespace Ticketing_backend.DTOs.Order;

public class CreateOrderRequest
{
    [Required(ErrorMessage = "User is required.")]
    public required Guid UserId { get; set; }

    [Required(ErrorMessage = "Order items are required.")]
    public required List<CreateOrderItemRequest> Items { get; set; }
}