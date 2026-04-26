// Mappings/OrderItemMappings.cs
using Ticketing_backend.DTOs.Order;
using Ticketing_backend.Models.Orders;

namespace Ticketing_backend.Mappings;

public static class OrderItemMappings
{
    public static OrderItemResponse ToResponse(this OrderItem orderItem) => new()
    {
        Id = orderItem.Id,
        TicketTypeId = orderItem.TicketTypeId,
        TicketTypeName = orderItem.TicketType?.Name ?? string.Empty,
        EventName = orderItem.TicketType?.Event?.Title ?? string.Empty,
        Quantity = orderItem.Quantity,
        PriceAtPurchase = orderItem.PriceAtPurchase,
        Tickets = orderItem.Tickets?.Select(t => t.ToResponse()).ToList() ?? []
    };

    public static OrderItem ToModel(this CreateOrderItemRequest request, Guid orderId) => new()
    {
        OrderId = orderId,
        TicketTypeId = request.TicketTypeId,
        Quantity = request.Quantity,
        PriceAtPurchase = 0 // calculated in service
    };
}