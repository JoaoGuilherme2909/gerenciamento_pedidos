using System.ComponentModel.DataAnnotations;
namespace gerenciamento_pedidos.api.Models;

public class User 
{
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string Role { get; set; }
}
