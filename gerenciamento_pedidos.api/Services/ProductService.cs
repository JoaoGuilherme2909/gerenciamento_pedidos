﻿using AutoMapper;
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
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SelectProductDto> CreateProduct(CreateProductDto productDto)
    {
        var produto = await _context.Products
            .FirstOrDefaultAsync(p => 
                p.Name.ToUpper().Equals(productDto.name.ToUpper())
            );

        if (produto is not null)
        {
            throw new Exception("Produto já existe");
        }

        var produtoCriado = _mapper.Map<Product>(productDto);
        await _context.AddAsync(produtoCriado);
        await _context.SaveChangesAsync();

        return _mapper.Map<SelectProductDto>(produtoCriado);
    }

    public async Task<ICollection<SelectProductDto>> SelectAllProducts()
    {
        var produtos = await _context.Products.ToListAsync();
        return produtos.Select(p => _mapper.Map<SelectProductDto>(p)).ToList();
    }

    public async Task<SelectProductDto> SelectProductsByName(string name)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Name.Equals(name));
        if(product is not null)
        {
            return _mapper.Map<SelectProductDto>(product);
        }

        throw new Exception("Produto não encontrado");
    
    }

    public async Task<Product> SelectProductById(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

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
            await _context.SaveChangesAsync();
        }
    }

    public async Task<SelectProductDto> UpdateProduct(Guid id, UpdateProductDto productDto)
    {
        var product = await SelectProductById(id);

        product.Name = productDto.name;
        product.Price = productDto.price;
        product.CategoryId = productDto.categoryId;

        await _context.SaveChangesAsync();
        return _mapper.Map<SelectProductDto>(product);


    }

    public async Task<ICollection<SelectProductDto>> GetProductsByCategory(int categoryId)
    {
        var products = await _context.Products.Include(p => p.Category)
            .Where(p => p.CategoryId == categoryId)
            .Select(p => _mapper.Map<SelectProductDto>(p))
            .ToListAsync();
        return products;
    }
}
