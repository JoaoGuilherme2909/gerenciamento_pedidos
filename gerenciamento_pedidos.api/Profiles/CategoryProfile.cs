using AutoMapper;
using gerenciamento_pedidos.api.Dtos.Category;
using gerenciamento_pedidos.api.Models;

namespace gerenciamento_pedidos.api.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<Category, SelectCategoryDto>();
    }
}
