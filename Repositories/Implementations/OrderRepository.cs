using Ticketing_backend.Models.Orders;
using Ticketing_backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;

namespace Ticketing_backend.Repositories.Implementations;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    public async Task<Order?> GetByReferenceNumberAsync(string referenceNumber) =>
        await _dbSet.FirstOrDefaultAsync(o => o.ReferenceNumber == referenceNumber);

    public async Task<Order?> GetByTransactionIdAsync(string transactionId) =>
        await _dbSet.FirstOrDefaultAsync(o => o.TransactionId == transactionId);

    public async Task<IEnumerable<Order>> GetByUserIdAsync(Guid id) =>
        await _dbSet.Where(o => o.UserId == id).ToListAsync();

    public async Task<IEnumerable<Order>> GetByTotalAmountAsync(decimal totalAmount) =>
        await _dbSet.Where(o => o.TotalAmount == totalAmount).ToListAsync();

    public async Task<IEnumerable<Order>> GetByMinTotalAmountAsync(decimal minTotalAmount) =>
        await _dbSet.Where(o => o.TotalAmount >= minTotalAmount).ToListAsync();

    public async Task<IEnumerable<Order>> GetByMaxTotalAmountAsync(decimal maxTotalAmount) =>
        await _dbSet.Where(o => o.TotalAmount <= maxTotalAmount).ToListAsync();

    public async Task<IEnumerable<Order>> GetByTotalAmountRangeAsync(decimal minTotalAmount, decimal maxTotalAmount) =>
        await _dbSet.Where(o => o.TotalAmount >= minTotalAmount && o.TotalAmount <= maxTotalAmount).ToListAsync();

    public async Task<IEnumerable<Order>> GetByStatusAsync(PaymentStatus status) =>
        await _dbSet.Where(o => o.Status == status).ToListAsync();

    public async Task<Order?> GetWithOrderItemsAsync(Guid id) =>
        await _dbSet.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
}