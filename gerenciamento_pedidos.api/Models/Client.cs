using System.ComponentModel.DataAnnotations;

namespace gerenciamento_pedidos.api.Models;

public class Client
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    public int TableId { get; set; }
    
    public virtual Table Table { get; set; }

    //Implementar posteriormente
    //public Guid UserId { get; set; }

    //public virtual User User { get; set; }

    public virtual ICollection<Order> Orders { get; set; }


}
