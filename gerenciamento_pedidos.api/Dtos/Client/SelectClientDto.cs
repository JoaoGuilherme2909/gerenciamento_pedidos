using gerenciamento_pedidos.api.Dtos.Table;

namespace gerenciamento_pedidos.api.Dtos.Client;

public record SelectClientDto(Guid id,string name, SelectTableDto table);
