using Microsoft.Extensions.DependencyInjection;
using OrderAggregator.Dtos;
using OrderAggregator.Services;

namespace OrderAggregator.Tests;

public class OrderServiceTests
{
    private IOrderService _orderService;
    
    [SetUp]
    public void Setup()
    {
        var services = ServiceBuilder.GetServiceProvider();
        _orderService = services.GetRequiredService<IOrderService>();
    }
    
    [Test]
    public async Task AddSingleOrderTest()
    {
        // Arrange
        var order = new OrderDto()
        {
            ProductId = "444",
            Quantity = 5,
        };

        // Act
        await _orderService.AddOrderAsync(order);
        var orders = await _orderService.GetAllOrdersAsync();

        // Assert
        Assert.That(orders, Has.Count.EqualTo(1));
        
        Assert.Multiple(() =>
        {
            Assert.That(orders[0].ProductId, Is.EqualTo(order.ProductId));
            Assert.That(orders[0].Quantity, Is.EqualTo(order.Quantity));
        });
    }
    
    [Test]
    public async Task AddTwoOrdersTest()
    {
        // Arrange
        var order = new OrderDto()
        {
            ProductId = "444",
            Quantity = 5,
        };

        // Act
        await _orderService.AddOrderAsync(order);
        await _orderService.AddOrderAsync(order);
        var orders = await _orderService.GetAllOrdersAsync();

        // Assert
        Assert.That(orders, Has.Count.EqualTo(1));
        
        Assert.Multiple(() =>
        {
            Assert.That(orders[0].ProductId, Is.EqualTo(order.ProductId));
            Assert.That(orders[0].Quantity, Is.EqualTo(order.Quantity * 2));
        });
    }

    [Test]
    public async Task AddOrdersParallel()
    {
        // Arrange
        const string productId = "444";
        const int quantity = 4;
        const int orderCount = 5000;
        
        var orders = new List<OrderDto>();
        for (var i = 0; i < orderCount; i++)
            orders.Add(new()
            {
                ProductId = productId,
                Quantity = quantity,
            });

        // Act
        await Parallel.ForEachAsync(orders, async (order, _) 
            => await _orderService.AddOrderAsync(order));

        // Assert
        var actualOrders = await _orderService.GetAllOrdersAsync();

        Assert.That(actualOrders, Has.Count.EqualTo(1));
        
        Assert.Multiple(() =>
        {
            Assert.That(actualOrders[0].ProductId, Is.EqualTo(productId));
            Assert.That(actualOrders[0].Quantity, Is.EqualTo(orderCount * quantity));
        });

    }
    
    
    [Test]
    public async Task AddOrdersWithMultipleProductsParallel()
    {
        // Arrange
        const string product1Id = "444";
        const string product2Id = "666";
        const int quantity1 = 4;
        const int quantity2 = 1;
        const int orderCount = 5000;
        
        var orders = new List<OrderDto>();
        for (var i = 0; i < orderCount; i++)
        {
            orders.Add(new()
            {
                ProductId = product1Id,
                Quantity = quantity1,
            });
            
            orders.Add(new()
            {
                ProductId = product2Id,
                Quantity = quantity2,
            });
        }

        // Act
        await Parallel.ForEachAsync(orders, async (order, _) 
            => await _orderService.AddOrderAsync(order));

        // Assert
        var actualOrders = await _orderService.GetAllOrdersAsync();

        Assert.That(actualOrders, Has.Count.EqualTo(2));
        
        var firstOrder = actualOrders.First(o => o.ProductId == product1Id);
        Assert.That(firstOrder.Quantity, Is.EqualTo(orderCount * quantity1));
       
        var secondOrder = actualOrders.First(o => o.ProductId == product2Id); 
        Assert.That(secondOrder.Quantity, Is.EqualTo(orderCount * quantity2));

    }
}