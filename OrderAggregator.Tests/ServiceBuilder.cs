using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderAggregator.Dtos;
using OrderAggregator.Services;
using OrderAggregator.Storage;

namespace OrderAggregator.Tests;

public class ServiceBuilder
{
    public static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        
        services.AddLogging(op =>
        {
            op.AddConsole();
            op.SetMinimumLevel(LogLevel.Trace);
        });
        
        services.AddSingleton<IStorage, MemoryStorage>();
        services.AddSingleton<ISender, SavingSender>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<ICollectorService, CollectorService>();

        return services.BuildServiceProvider();
    }
}

public class SavingSender : ISender
{
    public readonly List<OrderDto> Orders = new();
    public Task SendAsync(List<OrderDto> order)
    {
        Orders.AddRange(order);
        return Task.CompletedTask;
    }
}