using gerenciamento_pedidos.api.Dtos.Order;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _service;

    public OrderController( OrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto) 
    {
        await _service.CreateOrder(createOrderDto);
        return Ok("Pedido criado com sucesso!");    
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders() 
    {
        return Ok(await _service.GetAllOrders());
    }
}
