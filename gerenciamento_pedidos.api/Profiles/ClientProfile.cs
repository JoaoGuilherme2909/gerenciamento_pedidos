using AutoMapper;
using gerenciamento_pedidos.api.Dtos.Client;
using gerenciamento_pedidos.api.Models;

namespace gerenciamento_pedidos.api.Profiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<CreateClientDto, Client>();
        CreateMap<Client, SelectClientDto> ();
    }
}
