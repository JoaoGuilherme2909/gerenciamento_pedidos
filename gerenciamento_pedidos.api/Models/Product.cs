using System.ComponentModel.DataAnnotations;

namespace gerenciamento_pedidos.api.Models;

public class Product
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public double Price { get; set; }
    [Required]
    public bool Active { get; set; } = false;

    public int? CategoryId { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }


}
