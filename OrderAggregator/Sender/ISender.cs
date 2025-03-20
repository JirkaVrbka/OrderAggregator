using OrderAggregator.Dtos;

namespace OrderAggregator.Sender;

/// <summary>
/// Wrapper handling API sending of orders to an external service
/// </summary>
public interface ISender
{
    Task SendAsync(List<OrderDto> order);
}