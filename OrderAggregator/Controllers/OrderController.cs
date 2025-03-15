using Microsoft.AspNetCore.Mvc;
using OrderAggregator.Dtos;
using OrderAggregator.Services;

namespace OrderAggregator.Controllers;

[ApiController]
public class OrderController(IOrderService orderService)
{
    [HttpPut("/add-orders")]
    public async Task<ActionResult> AddOrders(List<OrderDto> orders)
    {
        await orderService.AddOrdersAsync(orders);
        return new OkResult();
    }
    
    [HttpPut("/add-order")]
    public async Task<ActionResult> AddOrder(OrderDto order)
    {
        await orderService.AddOrderAsync(order);
        return new OkResult();
    }

    [HttpGet("/get-orders")]
    public async Task<List<OrderDto>> GetOrders()
    {
        return await orderService.GetAllOrdersAsync();
    }
}