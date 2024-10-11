using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Table;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;
//TODO: Finalizar crud de mesa
[ApiController]
[Route("[Controller]")]
public class TableController : ControllerBase
{
    private readonly TableService _service;
    private readonly IMapper _mapper;

    public TableController(TableService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTable([FromBody] CreateTableDto createTableDto)
    {
        await _service.CreateTable(createTableDto);
        return Ok("Mesa criada com sucesso.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        return Ok(await _service.GetAllTables());
    }

    [HttpGet("{number}")]
    public async Task<IActionResult> GetTableByNumber([FromRoute] int number)
    {
        return Ok(await _service.GetTableByNumber(number));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable([FromRoute]int id) 
    { 
        await _service.DeleteTable(id);

        return NoContent();
    }

}
