using OrderAggregator.Dtos;

namespace OrderAggregator;

public interface ISender
{
    Task SendAsync(List<OrderDto> order);
}

// Console sender is here for demo only
public class ConsoleSender : ISender
{
    public Task SendAsync(List<OrderDto> order)
    {
        order.ForEach(Console.WriteLine);
        return Task.CompletedTask;
    }
}