using OrderAggregator.Dtos;

namespace OrderAggregator.Sender;

public class ConsoleSender : ISender
{
    public Task SendAsync(List<OrderDto> order)
    {
        order.ForEach(Console.WriteLine);
        return Task.CompletedTask;
    }
}