using AutoMapper;
using gerenciamento_pedidos.api.Dtos.User;
using gerenciamento_pedidos.api.Models;

namespace gerenciamento_pedidos.api.Profiles;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDTO, User>();
    }
}
