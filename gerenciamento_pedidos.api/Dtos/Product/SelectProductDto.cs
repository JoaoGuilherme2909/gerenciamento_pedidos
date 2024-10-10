using gerenciamento_pedidos.api.Dtos.Category;

namespace gerenciamento_pedidos.api.Dtos.Product;

public record SelectProductDto(Guid id,string name, double price, bool active, SelectCategoryDto Category);
