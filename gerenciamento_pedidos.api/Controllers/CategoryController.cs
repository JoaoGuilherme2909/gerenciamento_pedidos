using AutoMapper;
using gerenciamento_pedidos.api.Dtos.Category;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
[EnableCors("Policy")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;
    private readonly IMapper _mapper;

    public CategoryController(CategoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
    {
        await _service.CreateCategory(categoryDto);

        return Ok("Categoria criada com sucesso");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _service.GetAllCategories());
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetCategoryByName([FromRoute] string name)
    {
        return Ok(await _service.GetCategoryByName(name));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await _service.DeleteCategory(id);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CreateCategoryDto categoryDto)
    {
        await _service.UpdateCategory(id, categoryDto);

        return NoContent();
    }
}
