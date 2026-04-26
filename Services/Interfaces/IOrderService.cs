using Ticketing_backend.DTOs.Order;

namespace Ticketing_backend.Services.Interfaces;

public interface IOrderService : IService<OrderResponse, CreateOrderRequest, UpdateOrderRequest>
{
    Task<OrderResponse?> GetByReferenceNumberAsync(string referenceNumber);
    Task<IEnumerable<OrderResponse>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<OrderResponse>> GetByStatusAsync(PaymentStatus status);
}