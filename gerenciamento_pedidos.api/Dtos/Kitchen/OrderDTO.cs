namespace gerenciamento_pedidos.api.Dtos.Kitchen;

public class OrderDto
{
    public int Id { get; set; }
    public ClientDto Client { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<OrderProductDto> OrderProducts { get; set; }
}
