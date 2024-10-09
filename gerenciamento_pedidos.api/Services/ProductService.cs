using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Product;
using gerenciamento_pedidos.api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

    public async Task CreateProduct(CreateProductDto productDto)
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
    }

    public async Task<ICollection<SelectProductDto>> SelectAllProducts()
    {
        var produtos = await _appDbContext.Products.Where(p => p.Active == true).ToListAsync();
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

    private async Task<Product> SelectProductById(Guid id)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        if(product is not null)
        {
            return product;
        }

        throw new Exception("Produto não encontrado");
    }

    public async Task ToogleActiveProduct(Guid id)
    {
        var product = await SelectProductById(id);   
        
        if(product is not null)
        {
            product.Active = !product.Active;
            await _appDbContext.SaveChangesAsync();
        }
    }

    public async Task<SelectProductDto> UpdateProduct(Guid id, UpdateProductDto productDto)
    {
        var product = await SelectProductById(id);

        product.Name = productDto.name;
        product.Price = productDto.price;
        product.CategoryId = productDto.categoryId;

        await _appDbContext.SaveChangesAsync();
        return _mapper.Map<SelectProductDto>(product);


    }
}
