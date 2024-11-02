using AutoMapper;
using gerenciamento_pedidos.api.Dtos.Product;
using gerenciamento_pedidos.api.Models;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
[EnableCors("Policy")]
public class ProductController: ControllerBase
{
    private readonly ProductService _service;
    private readonly IMapper _mapper;

    public ProductController(ProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        
        await _service.CreateProduct(productDto);
        
        return Ok("Produto criado com sucesso");
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

    [HttpPut("/toggle_active_product/{id}")]
    public async Task<IActionResult> ToogleActiveProduct([FromRoute] Guid id)
    {
        await _service.ToogleActiveProduct(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id,[FromBody] JsonPatchDocument<UpdateProductDto> patch)
    {
        //TODO: Add JsonPatchDocument update
        var product = await _service.SelectProductById(id);

        var productToUpdate = _mapper.Map<UpdateProductDto>(product);

        patch.ApplyTo(productToUpdate, ModelState);

        if (!TryValidateModel(productToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        var result = await _service.UpdateProduct(id, productToUpdate);
        
        return Ok(result);
    }

    [HttpGet("Categoria/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategory([FromRoute] int categoryId)
    {
        var products = await _service.GetProductsByCategory(categoryId);
        return Ok(products);
    }
}
