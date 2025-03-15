using OrderAggregator.Dtos;

namespace OrderAggregator.Storage;

public interface IStorage
{
    Task AddAsync(OrderDto order);
    Task<OrderDto> GetAsync(string orderId);
    Task<List<OrderDto>> GetAllAsync();
    Task<List<OrderDto>> GetAllAndClearAsync();
    Task ClearAsync();
}