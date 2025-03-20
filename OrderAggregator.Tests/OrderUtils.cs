using OrderAggregator.Dtos;

namespace OrderAggregator.Tests;

public class OrderUtils
{
    public static OrderDto Generate()
        => new OrderDto()
        {
            ProductId = DateTime.Now.ToString(),
            Quantity = 5
        };
    
    public static OrderDto Generate(string productId, int quantity)
        => new OrderDto()
        {
            ProductId = productId,
            Quantity = quantity
        };

    public static List<OrderDto> GenerateMultiple(int repeat, string productId, int quantity)
    {
        var orders = new List<OrderDto>();
        for (var i = 0; i < repeat; i++)
            orders.Add(new()
            {
                ProductId = productId,
                Quantity = quantity,
            });
        
        return orders;
    }
    
    public static List<OrderDto> GenerateMultiple(int repeat, string productId)
    {
        var rand = new Random();
        var orders = new List<OrderDto>();
        for (var i = 0; i < repeat; i++)
            orders.Add(new()
            {
                ProductId = productId,
                Quantity = rand.Next(10),
            });
        
        return orders;
    }
    
    public static List<OrderDto> GenerateMultiple(int repeat)
    {
        var rand = new Random();
        var orders = new List<OrderDto>();
        for (var i = 0; i < repeat; i++)
            orders.Add(new()
            {
                ProductId = rand.Next(10).ToString(),
                Quantity = rand.Next(10)
            });
        
        return orders;
    }
    
        
    public static List<OrderDto> GenerateMultipleWithUniqueIds(int repeat)
    {
        var rand = new Random();
        var orders = new List<OrderDto>();
        for (var i = 0; i < repeat; i++)
            orders.Add(new()
            {
                ProductId = i.ToString(),
                Quantity = rand.Next(10)
            });
        
        return orders;
    }
}