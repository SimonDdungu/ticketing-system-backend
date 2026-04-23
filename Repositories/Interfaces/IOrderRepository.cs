using Ticketing_backend.Models.Orders;
namespace Ticketing_backend.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order?> GetByReferenceNumberAsync(string referenceNumber);

    Task<Order?> GetByTransactionIdAsync(string transactionId);

    Task<IEnumerable<Order>> GetByUserIdAsync(Guid id);

    Task<IEnumerable<Order>> GetByTotalAmountAsync(decimal totalAmount);

    Task<IEnumerable<Order>> GetByMinTotalAmountAsync(decimal minTotalAmount);

    Task<IEnumerable<Order>> GetByMaxTotalAmountAsync(decimal maxTotalAmount);

    Task<IEnumerable<Order>> GetByTotalAmountRangeAsync(decimal minTotalAmount, decimal maxTotalAmount);

    Task<IEnumerable<Order>> GetByStatusAsync(PaymentStatus status);

     Task<Order?> GetWithOrderItemsAsync(Guid id);

}