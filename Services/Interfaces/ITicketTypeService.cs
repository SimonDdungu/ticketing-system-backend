using Ticketing_backend.DTOs.TicketType;

namespace Ticketing_backend.Services.Interfaces;

public interface ITicketTypeService : IService<TicketTypeResponse, CreateTicketTypeRequest, UpdateTicketTypeRequest>
{
    Task<IEnumerable<TicketTypeResponse>> GetByEventIdAsync(Guid eventId);
    Task<IEnumerable<TicketTypeResponse>> GetByNameAsync(string name);
    Task<IEnumerable<TicketTypeResponse>> GetByPriceAsync(decimal price);
    Task<IEnumerable<TicketTypeResponse>> GetByMinPriceAsync(decimal minPrice);
    Task<IEnumerable<TicketTypeResponse>> GetByMaxPriceAsync(decimal maxPrice);
    Task<IEnumerable<TicketTypeResponse>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<TicketTypeResponse>> GetByMinQuantityRemainingAsync(int minQuantityRemaining);
    Task<IEnumerable<TicketTypeResponse>> GetByMaxQuantityRemainingAsync(int maxQuantityRemaining);
}