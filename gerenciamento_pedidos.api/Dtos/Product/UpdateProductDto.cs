namespace gerenciamento_pedidos.api.Dtos.Product;

public record UpdateProductDto(string name, double price, int? categoryId);
