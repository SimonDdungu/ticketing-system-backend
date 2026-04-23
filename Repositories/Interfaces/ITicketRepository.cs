using Ticketing_backend.Models.Tickets;
namespace Ticketing_backend.Repositories.Interfaces;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(Guid ticketTypeId);

    Task<IEnumerable<Ticket>> GetByEventIdAsync(Guid eventId);

    Task<Ticket?> GetByQRCodeAsync(string qrCode);

    Task<IEnumerable<Ticket>> GetByOrderItemAsync(Guid orderItemID);

    Task<IEnumerable<Ticket>> GetByIsScannedAsync(bool IsScanned);
    
}