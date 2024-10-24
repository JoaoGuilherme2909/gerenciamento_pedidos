using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Category;
using gerenciamento_pedidos.api.Models;
using Microsoft.EntityFrameworkCore;

namespace gerenciamento_pedidos.api.Services;

public class CategoryService
{

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SelectCategoryDto> CreateCategory(CreateCategoryDto categoryDto)
    {
        var category = await GetCategoryByName(categoryDto.name);

        if(category is not null)
        {
            throw new Exception("Categoria já existe");
        }

        var createdCategoty = _mapper.Map<Category>(categoryDto);
        await _context.Categories.AddAsync(createdCategoty);
        await _context.SaveChangesAsync();

        return _mapper.Map<SelectCategoryDto>(createdCategoty);

    }

    private async Task<Category> GetCategoryById(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        return category;
    }

    public async Task<SelectCategoryDto> GetCategoryByName(string name)
    {
        var category = await _context.Categories.
            FirstOrDefaultAsync(c => 
                c.Name.ToUpper().Equals(name.ToUpper())
            );
        return _mapper.Map<SelectCategoryDto>(category);
    }

    public async Task<ICollection<SelectCategoryDto>> GetAllCategories()
    {
        var categories = await _context.Categories.ToListAsync();

        return _mapper.Map<List<SelectCategoryDto>>(categories);
    }

    public async Task DeleteCategory(int id)
    {
        var category = await GetCategoryById(id);

        if (category is null)
        {
            throw new Exception("Categoria não encontrada");
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategory(int id, CreateCategoryDto categoryDto)
    {
        var category = await GetCategoryById(id);

        if (category is null)
        {
            throw new Exception("Categoria não encontrada");
        }

        category.Name = categoryDto.name;

        await _context.SaveChangesAsync();
    }
}
