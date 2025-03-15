using System.Collections.Concurrent;
using OrderAggregator.Dtos;

namespace OrderAggregator.Storage;

public class MemoryStorage : IStorage
{
    private readonly ConcurrentDictionary<string, int> _store = new();
    
    // Task is here to represent common storage that are async
    public Task AddAsync(OrderDto order)
    {
        _store.AddOrUpdate(order.ProductId, (_) => order.Quantity, (_, old) => old + order.Quantity);
        
        return Task.CompletedTask;
    }

    public Task<OrderDto> GetAsync(string orderId)
    {
        var order = new OrderDto { ProductId = orderId };
        if (_store.TryGetValue(orderId, out var quantity))
            order.Quantity = quantity;
        
        return Task.FromResult(order);
    }

    public Task<List<OrderDto>> GetAllAsync()
    {
        var orders = new List<OrderDto>();
        foreach (var (id, quantity) in _store)
        {
            orders.Add(new OrderDto { ProductId = id, Quantity = quantity });
        }
        
        return Task.FromResult(orders);
    }

    public Task<List<OrderDto>> GetAllAndClearAsync()
    {
        var orders = new List<OrderDto>();
        // copy to prevent loosing data in between convert and cleaning
        var storedValues = _store.ToArray();
        _store.Clear();
        
        foreach (var (id, quantity) in storedValues)
        {
            orders.Add(new OrderDto { ProductId = id, Quantity = quantity });
        }
        
        return Task.FromResult(orders);
    }

    public Task ClearAsync()
    {
        _store.Clear();
        return Task.CompletedTask;
    }
}