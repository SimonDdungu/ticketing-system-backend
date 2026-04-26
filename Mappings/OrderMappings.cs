
using Ticketing_backend.DTOs.Order;
using Ticketing_backend.Models.Orders;

namespace Ticketing_backend.Mappings;

public static class OrderMappings
{
    public static OrderResponse ToResponse(this Order order) => new()
    {
        Id = order.Id,
        UserId = order.UserId,
        ReferenceNumber = order.ReferenceNumber,
        TotalAmount = order.TotalAmount,
        Status = order.Status,
        TransactionId = order.TransactionId,
        Items = order.OrderItems?.Select(i => i.ToResponse()).ToList() ?? [],
        CreatedAt = order.CreatedAt,
        UpdatedAt = order.UpdatedAt
    };

    public static Order ToModel(this CreateOrderRequest request) => new()
    {
        UserId = request.UserId,
        TotalAmount = 0 // calculated in service
    };

    public static void UpdateModel(this Order order, UpdateOrderRequest request)
    {
        if (request.Status is not null) order.Status = request.Status.Value;
        if (request.TransactionId is not null) order.TransactionId = request.TransactionId;
    }
}