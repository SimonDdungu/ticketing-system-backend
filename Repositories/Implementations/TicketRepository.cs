using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Tickets;
using Ticketing_backend.Repositories.Interfaces;

namespace Ticketing_backend.Repositories.Implementations;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(AppDbContext context): base(context) {}

    public async Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(Guid ticketTypeId)
    {
        return await _dbSet.Where(t => t.TicketTypeId == ticketTypeId).ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByEventIdAsync(Guid eventId)
    {
        return await _dbSet.Where(t => t.EventId == eventId).ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByIsScannedAsync(bool IsScanned)
    {
        return await _dbSet.Where(t => t.IsScanned == IsScanned).ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByOrderItemAsync(Guid orderItemID)
    {
        return await _dbSet.Where(t => t.OrderItemId == orderItemID).ToListAsync();
    }

    public async Task<Ticket?> GetByQRCodeAsync(string qrCode)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.QRCode == qrCode);
    }

}