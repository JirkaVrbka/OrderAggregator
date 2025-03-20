using OrderAggregator.Dtos;

namespace OrderAggregator.Services;

/// <summary>
/// Service for keeping orders.
/// It is handling saving given orders into storage
/// </summary>
public interface IOrderService
{
    Task AddOrderAsync(OrderDto order);
    Task AddOrdersAsync(List<OrderDto> order);
    Task<List<OrderDto>> GetAllOrdersAsync();
    
}