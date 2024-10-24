using gerenciamento_pedidos.api.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace gerenciamento_pedidos.api.Models;

public class User : IdentityUser<Guid>
{
    public UserRole Role { get; set; }
}
