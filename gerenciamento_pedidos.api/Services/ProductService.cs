using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Product;
using gerenciamento_pedidos.api.Models;
using Microsoft.EntityFrameworkCore;

namespace gerenciamento_pedidos.api.Services;

public class ProductService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public ProductService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<SelectProductDto> CreateProduct(CreateProductDto productDto)
    {
        var produto = await _appDbContext.Products
            .FirstOrDefaultAsync(p => 
                p.Name.ToUpper().Equals(productDto.name)
            );

        if (produto is not null)
        {
            throw new Exception("Produto já existe");
        }
        
        var produtoCriado = await _appDbContext.AddAsync(_mapper.Map<Product>(productDto));

        await _appDbContext.SaveChangesAsync();

        return _mapper.Map<SelectProductDto>(produtoCriado);
    }

    public async Task<ICollection<SelectProductDto>> SelectAllProducts()
    {
        var produtos = await _appDbContext.Products.ToListAsync();
        return produtos.Select(p => _mapper.Map<SelectProductDto>(p)).ToList();
    }

    public async Task<ICollection<SelectProductDto>> SelectProductsByName(string name)
    {
        var products = await _appDbContext.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        if(products is not null)
        {
            return products.Select(p => _mapper.Map<SelectProductDto>(p)).ToList();
        }

        throw new Exception("Produto não encontrado");
    
    }

    public async Task ToogleActiveProduct(Guid id)
    {
        var produto = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);   
        
        if(produto is not null)
        {
            produto.Active = !produto.Active;
            await _appDbContext.SaveChangesAsync();
        }

        throw new Exception("Produto não encontrado");
    }

    public async Task UpdateProduct(Guid id, CreateProductDto productDto)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product is not null)
        {
            
        }
    }
}
