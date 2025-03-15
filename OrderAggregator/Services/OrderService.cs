using OrderAggregator.Dtos;
using OrderAggregator.Storage;

namespace OrderAggregator.Services;

public class OrderService(ILogger<OrderService> logger, IStorage storage) : IOrderService
{
    public async Task AddOrderAsync(OrderDto order)
    {
        logger.LogInformation("Adding to storage {Order}", order);
        await storage.AddAsync(order);
    }

    public async Task AddOrdersAsync(List<OrderDto> orders)
    {
        foreach (var order in orders)
        {
            logger.LogInformation("Adding to storage {Order}", order);
            await storage.AddAsync(order);
        }
    }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    {
        return await storage.GetAllAsync();
    }
}