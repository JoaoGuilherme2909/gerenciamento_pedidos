using System.ComponentModel.DataAnnotations;

namespace gerenciamento_pedidos.api.Models;

public class Table
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int Number {  get; set; }
    public bool IsBusy { get; set; } = false;
    public virtual ICollection<Client> Clients { get; set; }
}
