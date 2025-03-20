using OrderAggregator.Sender;
using OrderAggregator.Storage;

namespace OrderAggregator.Services;

public class CollectorService(ILogger<CollectorService> logger, IStorage storage, ISender sender) : ICollectorService
{
    public async Task CollectAsync()
    {
        logger.LogInformation("Collecting orders");

        var orders = await storage.GetAllAndClearAsync();
        
        if (orders.Any())
            await sender.SendAsync(orders);
    }
}