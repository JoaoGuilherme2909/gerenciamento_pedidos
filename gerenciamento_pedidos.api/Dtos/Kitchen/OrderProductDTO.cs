﻿namespace gerenciamento_pedidos.api.Dtos.Kitchen;

public class OrderProductDto
{
    public Guid ProductId { get; set; }
    public bool IsFinish { get; set; }
    public ProductDto Product { get; set; }
}
