﻿namespace gerenciamento_pedidos.api.Dtos.Product;

public record SelectProductDto(Guid id,string name, double price, bool active, int? categoryId);