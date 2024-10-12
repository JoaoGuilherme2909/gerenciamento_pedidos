namespace gerenciamento_pedidos.api.Dtos.Order;

public record CreateOrderDto(int id, Guid clientId, Guid productId);
