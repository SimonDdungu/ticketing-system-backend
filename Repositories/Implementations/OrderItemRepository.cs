using Ticketing_backend.Models.Orders;
using Ticketing_backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;


namespace Ticketing_backend.Repositories.Implementations;

public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId) =>
        await _dbSet.Where(oi => oi.OrderId == orderId).ToListAsync();

    public async Task<OrderItem?> GetWithTicketsAsync(Guid id) =>
        await _dbSet.Include(oi => oi.Tickets).FirstOrDefaultAsync(oi => oi.Id == id);
}