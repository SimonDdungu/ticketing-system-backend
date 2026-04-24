using Ticketing_backend.DTOs.Ticket;

namespace Ticketing_backend.Services.Interfaces;

public interface ITicketService
{
    Task<TicketResponse?> GetByIdAsync(Guid id);
    Task<TicketResponse?> GetByQRCodeAsync(string qrCode);
    Task<IEnumerable<TicketResponse>> GetByOrderItemIdAsync(Guid orderItemId);
    Task<IEnumerable<TicketResponse>> GetByEventIdAsync(Guid eventId);
    Task<TicketResponse> CreateAsync(CreateTicketRequest request);
    Task<TicketResponse> ScanAsync(Guid id);
}