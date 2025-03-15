using OrderAggregator.Dtos;

namespace OrderAggregator.Services;

public interface IOrderService
{
    Task AddOrderAsync(OrderDto order);
    Task AddOrdersAsync(List<OrderDto> order);
    Task<List<OrderDto>> GetAllOrdersAsync();
    
}