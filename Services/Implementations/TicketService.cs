using Ticketing_backend.DTOs.Ticket;
using Ticketing_backend.Mappings;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    private readonly IEventRepository _eventRepository;

    public TicketService(ITicketRepository ticketRepository, IOrderItemRepository orderItemRepository, IEventRepository eventRepository)
    {
        _ticketRepository = ticketRepository;
        _orderItemRepository = orderItemRepository;
        _eventRepository = eventRepository;
    }

    public async Task<TicketResponse?> GetByIdAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);
        return ticket?.ToResponse();
    }

    public async Task<TicketResponse?> GetByQRCodeAsync(string qrCode)
    {
        var ticket = await _ticketRepository.GetByQRCodeAsync(qrCode);
        return ticket?.ToResponse();
    }

    public async Task<IEnumerable<TicketResponse>> GetByOrderItemIdAsync(Guid orderItemId)
    {
        var tickets = await _ticketRepository.GetByOrderItemAsync(orderItemId);
        return tickets.Select(t => t.ToResponse());
    }

    public async Task<TicketResponse> CreateAsync(CreateTicketRequest request)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(request.OrderItemId);
        if (orderItem is null) throw new KeyNotFoundException($"OrderItem with id {request.OrderItemId} not found.");

        var Event = await _eventRepository.GetByIdAsync(request.EventId);
        if (Event is null) throw new KeyNotFoundException($"Event with id {request.EventId} not found.");

        var ticket = request.ToModel();

        _ticketRepository.Add(ticket);

        await _ticketRepository.SaveAsync();

        return ticket.ToResponse();
    }

    public async Task<TicketResponse> ScanAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null) throw new KeyNotFoundException($"Ticket with id {id} not found.");

        if (ticket.IsScanned) throw new InvalidOperationException("Ticket has already been scanned.");

        ticket.UpdateModel();

        _ticketRepository.Update(ticket);

        await _ticketRepository.SaveAsync();

        return ticket.ToResponse();
    }

    public async Task<IEnumerable<TicketResponse>> GetByEventIdAsync(Guid eventId)
    {
        var tickets = await _ticketRepository.GetByEventIdAsync(eventId);
        return tickets.Select(t => t.ToResponse());
    }
}