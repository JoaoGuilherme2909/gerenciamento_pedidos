namespace gerenciamento_pedidos.api.Dtos.Product;

public record CreateProductDto(string name, double price, int? categoryId);
