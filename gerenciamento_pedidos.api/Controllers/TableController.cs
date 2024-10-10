using AutoMapper;
using gerenciamento_pedidos.api.Data;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;
//TODO: Finalizar crud de mesa
[ApiController]
[Route("[Controller]")]
public class TableController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TableController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTableByNumber([FromRoute] int id)
    {
        return Ok();
    }


}
