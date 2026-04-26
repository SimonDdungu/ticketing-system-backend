using Microsoft.EntityFrameworkCore;
using Ticketing_backend.Data;
using Ticketing_backend.Models.Tickets;
using Ticketing_backend.Repositories.Interfaces;
namespace Ticketing_backend.Repositories.Implementations;

public class TicketTypeRepository : Repository<TicketType>, ITicketTypeRepository
{
    public TicketTypeRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<TicketType>> GetByEventIdAsync(Guid eventId) =>
        await _dbSet.Where(t => t.EventId == eventId).ToListAsync();

    public async Task<TicketType?> GetByIdWithEventAsync(Guid id) =>
        await _dbSet.Include(t => t.Event).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<TicketType>> GetByNameAsync(string name) =>
        await _dbSet.Where(t => t.Name.Contains(name)).ToListAsync();

    public async Task<IEnumerable<TicketType>> GetByPriceAsync(decimal price) =>
        await _dbSet.Where(t => t.Price == price).ToListAsync();

    public async Task<IEnumerable<TicketType>> GetByMinPriceAsync(decimal minPrice) =>
        await _dbSet.Where(t => t.Price >= minPrice).ToListAsync();

    public async Task<IEnumerable<TicketType>> GetByMaxPriceAsync(decimal maxPrice) =>
        await _dbSet.Where(t => t.Price <= maxPrice).ToListAsync();

    public async Task<IEnumerable<TicketType>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice) =>
        await _dbSet.Where(t => t.Price >= minPrice && t.Price <= maxPrice).ToListAsync();

    public async Task<IEnumerable<TicketType>> GetByMinQuantityRemainingAsync(int minQuantityRemaining) =>
        await _dbSet.Where(t => t.QuantityRemaining >= minQuantityRemaining).ToListAsync();

    public async Task<IEnumerable<TicketType>> GetByMaxQuantityRemainingAsync(int maxQuantityRemaining) =>
        await _dbSet.Where(t => t.QuantityRemaining <= maxQuantityRemaining).ToListAsync();
}