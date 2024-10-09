using System.ComponentModel.DataAnnotations;

namespace gerenciamento_pedidos.api.Models;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public bool paid { get; set; } = false;

    [Required]
    public Guid ClientId { get; set; }

    [Required]
    public virtual Client Client { get; set; }

    public virtual ICollection<Product> Products { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
