using Microsoft.Extensions.DependencyInjection;
using OrderAggregator.Dtos;
using OrderAggregator.Sender;
using OrderAggregator.Services;

namespace OrderAggregator.Tests;

public class CollectorServiceTests
{
    private IOrderService _orderService;
    private ICollectorService _collectorService;
    private SavingSender _savingSender;
    
    [SetUp]
    public void Setup()
    {
        var services = ServiceBuilder.GetServiceProvider();
        _orderService = services.GetRequiredService<IOrderService>();
        _collectorService = services.GetRequiredService<ICollectorService>();
        _savingSender = (services.GetRequiredService<ISender>() as SavingSender)!;
    }

    [Test]
    public async Task ClearAfterCollectedTest()
    {
        // Arrange
        await _orderService.AddOrdersAsync(OrderUtils.GenerateMultipleWithUniqueIds(10));

        // Act
        await _collectorService.CollectAsync();

        // Assert
        var orders = await _orderService.GetAllOrdersAsync();
        Assert.That(orders, Is.Empty);
        Assert.That(_savingSender.Orders, Has.Count.EqualTo(10));
    }


}