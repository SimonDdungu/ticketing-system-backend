using Ticketing_backend.Models.Tickets;
namespace Ticketing_backend.Repositories.Interfaces;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Ticket>> GetAllAsync();
    
    void Add(Ticket entity);
    
    void Update(Ticket entity);
    
    Task SaveAsync();
    
    Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(Guid ticketTypeId);
    
    Task<IEnumerable<Ticket>> GetByEventIdAsync(Guid eventId);
    
    Task<Ticket?> GetByQRCodeAsync(string qrCode);
    
    Task<IEnumerable<Ticket>> GetByOrderItemAsync(Guid orderItemId);
    
    Task<IEnumerable<Ticket>> GetByIsScannedAsync(bool isScanned);
}