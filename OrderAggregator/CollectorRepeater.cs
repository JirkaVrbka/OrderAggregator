using OrderAggregator.Services;

namespace OrderAggregator;

public class OrderSender(ILogger<OrderSender> logger, ICollectorService collector) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(20); 
    private static readonly PeriodicTimer Timer = new(Interval);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogTrace("Starting order sender");
        
        while (await Timer.WaitForNextTickAsync(stoppingToken))
            await collector.CollectAsync();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogTrace("Stopping order sender");
        Timer.Dispose();
        await base.StopAsync(cancellationToken);
    }
}