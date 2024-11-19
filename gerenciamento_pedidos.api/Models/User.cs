using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace gerenciamento_pedidos.api.Models;

public class User : IdentityUser
{
   public string Role { get; set; }
    public User(): base()
    {
        
    }
}
