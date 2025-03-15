namespace OrderAggregator.Dtos;

public class OrderDto
{
    public required string ProductId { get; init; }
    public int Quantity { get; set; }

    public override string ToString()
        => $"Order '{ProductId}' with quantity '{Quantity}'";
}