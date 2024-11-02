using gerenciamento_pedidos.api.Dtos.Client;
using gerenciamento_pedidos.api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace gerenciamento_pedidos.api.Controllers;

[ApiController]
[Route("[Controller]")]
[EnableCors("Policy")]
public class ClientController : ControllerBase
{
    private readonly ClientService _service;

    public ClientController(ClientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllClients()
    {
        return Ok(await _service.GetAllClients());
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientDto clientDto)
    {
        await _service.CreateClient(clientDto);

        return Ok("Cliente criado com sucesso");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        await _service.DeleteClient(id);

        return NoContent();
    }
}
