﻿using gerenciamento_pedidos.api.Dtos.Product;

namespace gerenciamento_pedidos.api.Dtos.Order;

public class SelectOrderDto
{
    public int id {  get; set; }
    public Guid clientId { get; set; }
    public ICollection<MiniSelectProductDto> products { get; set; }
    public SelectOrderDto()
    {
        
    }

}
