using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Client;
using gerenciamento_pedidos.api.Models;
using Microsoft.EntityFrameworkCore;

namespace gerenciamento_pedidos.api.Services;

public class ClientService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ClientService(AppDbContext context, IMapper mapper)
    {
        _context = context; 
        _mapper = mapper;
    }

    public async Task<ICollection<SelectClientDto>> GetAllClients()
    {
        var clients = await _context.Clients.Include(c => c.Table).Select(c => _mapper.Map<SelectClientDto>(c)).ToListAsync();

        return clients;
    }

    public async Task<Client> GetClientById(Guid id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        return client;
    }

    public async Task CreateClient(CreateClientDto clientDto)
    {
        var client = _mapper.Map<Client>(clientDto);
        await _context.Clients.AddAsync(client);

        var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == clientDto.tableId);

        table.IsBusy = true;

        await _context.SaveChangesAsync();

        var order = new Order() { ClientId = client.Id};
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClient(Guid id)
    {
        var client = await GetClientById(id);

        if (client is null) 
        {
            throw new Exception("Cliente nao encontrado");
        }

        client.Active = false;

        var table = await _context.Tables
                .Include(t => t.Clients.Where(c => c.Active == true))
                .FirstOrDefaultAsync(t => t.Id == client.TableId);

        if (table.Clients.Count == 1)
        {
            table.IsBusy = false;
        }

        await _context.SaveChangesAsync();
    }

}
