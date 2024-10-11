using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Table;
using gerenciamento_pedidos.api.Models;
using Microsoft.EntityFrameworkCore;

namespace gerenciamento_pedidos.api.Services;

public class TableService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TableService(AppDbContext context , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateTable(CreateTableDto createTableDto) 
    {
        var table = await _context.Tables
               .FirstOrDefaultAsync(
                    t => t.Number == createTableDto.number
                );

        if (table is not null)
        {
            throw new Exception("Mesa já criada.");
        }
        await _context.AddAsync(_mapper.Map<Table>(createTableDto));
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<SelectTableDto>> GetAllTables() 
    {
        var tables = await _context.Tables.ToListAsync();
        return _mapper.Map<List<SelectTableDto>>(tables);
    }
    
    public async Task<ICollection<SelectTableDto>> GetTableByNumber(int number) 
    {
        var tables = await _context.Tables.Where(t => t.Number == number).ToListAsync();
        if (tables is not null) 
        {
            return tables.Select(t => _mapper.Map<SelectTableDto>(t)).ToList();
        }

        throw new Exception("Mesa não encontrada.");
    }

    private async Task<Table> GetTableById(int id) 
    {
        var tables = await _context.Tables.FirstOrDefaultAsync(t => t.Id == id);

        return tables;
    }

    public async Task DeleteTable(int id)
    {
        var table = await GetTableById(id);

        if (table is null) {
            throw new Exception("Mesa não encontrada."); 
        }

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();
    }

}
