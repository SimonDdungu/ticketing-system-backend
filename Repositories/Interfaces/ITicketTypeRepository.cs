using Ticketing_backend.Models.Tickets;
namespace Ticketing_backend.Repositories.Interfaces;

public interface ITicketTypeRepository : IRepository<TicketType>
{
    Task<IEnumerable<TicketType>> GetByEventIdAsync(Guid eventId);

    Task<TicketType?> GetByIdWithEventAsync(Guid id);

    Task<IEnumerable<TicketType>> GetByNameAsync(string name);

    Task<IEnumerable<TicketType>> GetByPriceAsync(decimal price);

    Task<IEnumerable<TicketType>> GetByMinPriceAsync(decimal minPrice);

    Task<IEnumerable<TicketType>> GetByMaxPriceAsync(decimal maxPrice);

    Task<IEnumerable<TicketType>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);

    Task<IEnumerable<TicketType>> GetByMinQuantityRemainingAsync(int minQuantityRemaining);

    Task<IEnumerable<TicketType>> GetByMaxQuantityRemainingAsync(int maxQuantityRemaining);
    
}