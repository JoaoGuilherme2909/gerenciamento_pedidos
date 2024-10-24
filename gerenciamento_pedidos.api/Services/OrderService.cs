using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Order;
using gerenciamento_pedidos.api.Dtos.Product;
using gerenciamento_pedidos.api.Models;
using Microsoft.EntityFrameworkCore;

namespace gerenciamento_pedidos.api.Services;

public class OrderService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderService(AppDbContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ICollection<MiniSelectProductDto>> CreateOrder(CreateOrderDto createOrderDto) 
    {
        var order = await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == createOrderDto.id);

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == createOrderDto.productId);

        if (product == null) 
        {
            throw new Exception("Produto não encontrado.");
        }

        order.Products.Add(product);
 
        var orderCreated = _mapper.Map<SelectOrderDto>(order);
        await _context.SaveChangesAsync();

        return orderCreated.products;
    }

    public async Task<ICollection<SelectOrderDto>> GetAllOrders() 
    {
        var orders = await _context.Orders.AsNoTracking()
                                          .Include(o => o.Products)
                                          .Where(o => o.paid == false)
                                          .Select(o => _mapper.Map<SelectOrderDto>(o))
                                          .ToListAsync();

        return orders;
    }
}
