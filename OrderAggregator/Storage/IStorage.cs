using OrderAggregator.Dtos;

namespace OrderAggregator.Storage;

/// <summary>
/// Wrapper for actual work with storage API 
/// </summary>
public interface IStorage
{
    Task AddAsync(OrderDto order);
    Task<List<OrderDto>> GetAllAsync();
    Task<List<OrderDto>> GetAllAndClearAsync();
}