using OrderAggregator.Dtos;

namespace OrderAggregator.Sender;

public interface ISender
{
    Task SendAsync(List<OrderDto> order);
}