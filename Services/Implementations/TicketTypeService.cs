using Ticketing_backend.DTOs.SoftDelete;
using Ticketing_backend.DTOs.TicketType;
using Ticketing_backend.Mappings;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services;

public class TicketTypeService : ITicketTypeService
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IEventRepository _eventRepository;

    public TicketTypeService(ITicketTypeRepository ticketTypeRepository, IEventRepository eventRepository)
    {
        _ticketTypeRepository = ticketTypeRepository;
        _eventRepository = eventRepository;
    }

    public async Task<TicketTypeResponse?> GetByIdAsync(Guid id)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(id);
        return ticketType?.ToResponse();
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetAllAsync()
    {
        var ticketTypes = await _ticketTypeRepository.GetAllAsync();
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<TicketTypeResponse> CreateAsync(CreateTicketTypeRequest request)
    {
        var Event = await _eventRepository.GetByIdAsync(request.EventId);

        if (Event is null) throw new KeyNotFoundException($"Event with id {request.EventId} not found.");

        var ticketType = request.ToModel();

        _ticketTypeRepository.Add(ticketType);

        await _ticketTypeRepository.SaveAsync();

        return ticketType.ToResponse();
    }

    public async Task<TicketTypeResponse> UpdateAsync(Guid id, UpdateTicketTypeRequest request)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(id);
        if (ticketType is null) throw new KeyNotFoundException($"TicketType with id {id} not found.");

        ticketType.UpdateModel(request);

        ticketType.UpdatedAt = DateTime.UtcNow;

        _ticketTypeRepository.Update(ticketType);

        await _ticketTypeRepository.SaveAsync();

        return ticketType.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(id);
        if (ticketType is null) throw new KeyNotFoundException($"TicketType with id {id} not found.");

        _ticketTypeRepository.Delete(ticketType);
        
        await _ticketTypeRepository.SaveAsync();
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByEventIdAsync(Guid eventId)
    {
        var ticketTypes = await _ticketTypeRepository.GetByEventIdAsync(eventId);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByNameAsync(string name)
    {
        var ticketTypes = await _ticketTypeRepository.GetByNameAsync(name);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByPriceAsync(decimal price)
    {
        var ticketTypes = await _ticketTypeRepository.GetByPriceAsync(price);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByMinPriceAsync(decimal minPrice)
    {
        var ticketTypes = await _ticketTypeRepository.GetByMinPriceAsync(minPrice);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByMaxPriceAsync(decimal maxPrice)
    {
        var ticketTypes = await _ticketTypeRepository.GetByMaxPriceAsync(maxPrice);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var ticketTypes = await _ticketTypeRepository.GetByPriceRangeAsync(minPrice, maxPrice);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByMinQuantityRemainingAsync(int minQuantityRemaining)
    {
        var ticketTypes = await _ticketTypeRepository.GetByMinQuantityRemainingAsync(minQuantityRemaining);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task<IEnumerable<TicketTypeResponse>> GetByMaxQuantityRemainingAsync(int maxQuantityRemaining)
    {
        var ticketTypes = await _ticketTypeRepository.GetByMaxQuantityRemainingAsync(maxQuantityRemaining);
        return ticketTypes.Select(t => t.ToResponse());
    }

    public async Task SoftDeleteAsync(Guid id, SoftDeleteRequest request)
    {
        var ticketType = await _ticketTypeRepository.GetByIdAsync(id);
        if(ticketType is null) throw new KeyNotFoundException($"Ticket Type with id {id} can not be found");
        
        ticketType.IsDeleted = request.IsDeleted;
        ticketType.DeletedAt = request.IsDeleted ? DateTime.UtcNow : null;
        ticketType.UpdatedAt = DateTime.UtcNow;

        _ticketTypeRepository.Update(ticketType);

        await _ticketTypeRepository.SaveAsync();
    }
}