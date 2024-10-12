using AutoMapper;
using gerenciamento_pedidos.api.Models;
using gerenciamento_pedidos.api.Dtos.Order;

namespace gerenciamento_pedidos.api.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile() 
    {
        CreateMap<Order, SelectOrderDto>();
    }
}
