﻿using AutoMapper;
using gerenciamento_pedidos.api.Dtos.Product;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> ToogleActiveProduct([FromRoute] Guid id)
    {
        await _service.ToogleActiveProduct(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id,[FromBody] UpdateProductDto productDto)
    {
        //TODO: Add JsonPatchDocument update

        var result = await _service.UpdateProduct(id, productDto);
        
        return Ok(result);
    }
}