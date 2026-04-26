// Services/OrderService.cs
using Ticketing_backend.Data;
using Ticketing_backend.DTOs.Order;
using Ticketing_backend.DTOs.Ticket;
using Ticketing_backend.Mappings;
using Ticketing_backend.Models.Orders;
using Ticketing_backend.Repositories.Interfaces;
using Ticketing_backend.Services.Interfaces;

namespace Ticketing_backend.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly ITicketService _ticketService;

    private readonly AppDbContext _context;

    public OrderService (IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, ITicketTypeRepository ticketTypeRepository,  ITicketService ticketService, AppDbContext context)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _ticketTypeRepository = ticketTypeRepository;
        _ticketService = ticketService;
        _context = context;
    }

    public async Task<OrderResponse?> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order?.ToResponse();
    }

    public async Task<IEnumerable<OrderResponse>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(o => o.ToResponse());
    }

    public async Task<OrderResponse?> GetByReferenceNumberAsync(string referenceNumber)
    {
        var order = await _orderRepository.GetByReferenceNumberAsync(referenceNumber);
        return order?.ToResponse();
    }

    public async Task<IEnumerable<OrderResponse>> GetByUserIdAsync(Guid userId)
    {
        var orders = await _orderRepository.GetByUserIdAsync(userId);
        return orders.Select(o => o.ToResponse());
    }

    public async Task<IEnumerable<OrderResponse>> GetByStatusAsync(PaymentStatus status)
    {
        var orders = await _orderRepository.GetByStatusAsync(status);
        return orders.Select(o => o.ToResponse());
    }

    public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
    {
         using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Create the order
                var order = request.ToModel();
                _orderRepository.Add(order);
                await _orderRepository.SaveAsync();

                decimal totalAmount = 0;

                // 2. Process each item
                foreach (var itemRequest in request.Items)
                {
                    // fetch ticket type to get price
                    var ticketType = await _ticketTypeRepository.GetByIdWithEventAsync(itemRequest.TicketTypeId);
                    if (ticketType is null) 
                        throw new KeyNotFoundException($"TicketType with id {itemRequest.TicketTypeId} not found.");

                    if (ticketType.Event is null)
                        throw new KeyNotFoundException($"Event for TicketType {ticketType.Id} not found.");

                    // check availability
                    if (ticketType.QuantityRemaining < itemRequest.Quantity)
                        throw new InvalidOperationException($"Not enough tickets. Only {ticketType.QuantityRemaining} available");

                    // create order item
                    var orderItem = itemRequest.ToModel(order.Id);
                    orderItem.PriceAtPurchase = ticketType.Price;
                    _orderItemRepository.Add(orderItem);
                    await _orderItemRepository.SaveAsync();

                    // decrease quantity remaining
                    ticketType.QuantityRemaining -= itemRequest.Quantity;
                    _ticketTypeRepository.Update(ticketType);

                    // create tickets
                        for (int i = 0; i < itemRequest.Quantity; i++)
                        {
                            await _ticketService.CreateAsync(new CreateTicketRequest
                            {
                                TicketTypeId = itemRequest.TicketTypeId,
                                EventId = ticketType.Event.Id,
                                OrderItemId = orderItem.Id
                            });
                        }

                    totalAmount += ticketType.Price * itemRequest.Quantity;
                }

                // 3. Update total amount
                order.TotalAmount = totalAmount;
                _orderRepository.Update(order);
                await _orderRepository.SaveAsync();

                await transaction.CommitAsync();
                return order.ToResponse();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
    }

    public async Task<OrderResponse> UpdateAsync(Guid id, UpdateOrderRequest request)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException($"Order with id {id} not found.");
        order.UpdateModel(request);
        order.UpdatedAt = DateTime.UtcNow;
        _orderRepository.Update(order);
        await _orderRepository.SaveAsync();
        return order.ToResponse();
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException($"Order with id {id} not found.");
        _orderRepository.Delete(order);
        await _orderRepository.SaveAsync();
    }
}