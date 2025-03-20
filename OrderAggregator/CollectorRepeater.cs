using OrderAggregator.Services;

namespace OrderAggregator;

/// <summary>
/// Background service for repeated sending of orders to external system
/// </summary>
/// <param name="logger">Logger for service</param>
/// <param name="collector">Collector interface that is repeatably send</param>
public class OrderRepeater(ILogger<OrderRepeater> logger, ICollectorService collector) : BackgroundService
{
    private static readonly TimeSpan Interval = TimeSpan.FromSeconds(EnvironmentVariable.SecondsToSend); 
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