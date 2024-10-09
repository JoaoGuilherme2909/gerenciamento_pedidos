using System.ComponentModel.DataAnnotations;

namespace gerenciamento_pedidos.api.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual ICollection<Order> Orders { get;set; }
}
