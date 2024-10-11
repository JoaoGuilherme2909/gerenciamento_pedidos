using AutoMapper;
using gerenciamento_pedidos.api.Dtos.Table;
using gerenciamento_pedidos.api.Models;

namespace gerenciamento_pedidos.api.Profiles;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<CreateTableDto, Table>();
        CreateMap<SelectTableDto, Table>();
        CreateMap<Table, SelectTableDto>();
    }
}
