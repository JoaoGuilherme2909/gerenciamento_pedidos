using System.ComponentModel.DataAnnotations;

namespace gerenciamento_pedidos.api.Models;


public class OrderProduct
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Required]
    public Guid ProductId { get; set; }

    public bool IsFinish { get; set; } = false;

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}

