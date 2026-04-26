using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Tickets;
using Ticketing_backend.Repositories.Interfaces;

namespace Ticketing_backend.Repositories.Implementations;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Ticket> _dbSet;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Ticket>();
    }

     public async Task<Ticket?> GetByIdAsync(Guid id) =>
        await _dbSet.FindAsync(id);

    public async Task<IEnumerable<Ticket>> GetAllAsync() =>
        await _dbSet.ToListAsync();

    public void Add(Ticket entity) =>
        _dbSet.Add(entity);

    public void Update(Ticket entity) =>
        _dbSet.Update(entity);

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();

    public async Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(Guid ticketTypeId) =>
        await _dbSet.Where(t => t.TicketTypeId == ticketTypeId).ToListAsync();

    public async Task<IEnumerable<Ticket>> GetByEventIdAsync(Guid eventId) =>
        await _dbSet.Where(t => t.EventId == eventId).ToListAsync();

    public async Task<IEnumerable<Ticket>> GetByIsScannedAsync(bool isScanned) =>
        await _dbSet.Where(t => t.IsScanned == isScanned).ToListAsync();

    public async Task<IEnumerable<Ticket>> GetByOrderItemAsync(Guid orderItemId) =>
        await _dbSet.Where(t => t.OrderItemId == orderItemId).ToListAsync();

    public async Task<Ticket?> GetByQRCodeAsync(string qrCode) =>
        await _dbSet.FirstOrDefaultAsync(t => t.QRCode == qrCode);
}
