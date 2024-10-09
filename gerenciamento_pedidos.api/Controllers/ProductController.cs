using gerenciamento_pedidos.api.Dtos.Product;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
public class ProductController: ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        
        var result = await _service.CreateProduct(productDto);
        
        return Created($"/Product/{result.name}",result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _service.SelectAllProducts();

        return Ok(result);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetProductsByName([FromRoute] string name)
    {
        var result = await _service.SelectProductsByName(name);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ToogleActiveProduct([FromRoute] Guid id)
    {
        await _service.ToogleActiveProduct(id);
        return NoContent();
    }
}
