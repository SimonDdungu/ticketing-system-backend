using Ticketing_backend.Models.Orders;

namespace Ticketing_backend.Repositories.Interfaces;

public interface IOrderItemRepository : IRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId);

    Task<OrderItem?> GetWithTicketsAsync(Guid id);
}