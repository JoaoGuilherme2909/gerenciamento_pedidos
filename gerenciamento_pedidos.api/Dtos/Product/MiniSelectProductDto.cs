namespace gerenciamento_pedidos.api.Dtos.Product;

public class MiniSelectProductDto 
{
    public Guid Id { get; set; }   
    public string name { get; set; } 
    public double price { get; set; }

    public MiniSelectProductDto()
    {
        
    }
}
