using AutoMapper;
using gerenciamento_pedidos.api.Dtos.Product;
using gerenciamento_pedidos.api.Models;

namespace gerenciamento_pedidos.api.Profiles;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, SelectProductDto>();
        CreateMap<Product, MiniSelectProductDto>();
        CreateMap<SelectProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, UpdateProductDto>();
    }
}
