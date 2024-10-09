using AutoMapper;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;
    private readonly IMapper _mapper;

    public CategoryController(CategoryService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
}
