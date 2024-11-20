using AutoMapper;
using gerenciamento_pedidos.api.Data;
using gerenciamento_pedidos.api.Dtos.Kitchen;
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

        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == createOrderDto.id);

        if (order == null)
        {
            throw new Exception("Pedido não encontrado.");
        }


        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == createOrderDto.productId);

        if (product == null)
        {
            throw new Exception("Produto não encontrado.");
        }


        var orderProduct = new OrderProduct
        {
            OrderId = order.Id,
            ProductId = product.Id,
            IsFinish = false 
        };

        _context.Set<OrderProduct>().Add(orderProduct);

        await _context.SaveChangesAsync();


        var orderCreated = await _context.Orders
                                          .Include(o => o.OrderProducts)
                                          .ThenInclude(op => op.Product)
                                          .Where(o => o.Id == order.Id)
                                          .Select(o => _mapper.Map<SelectOrderDto>(o))
                                          .FirstOrDefaultAsync();

        return orderCreated?.products;
    }

    public async Task<ICollection<OrderDto>> GetAllOrders()
    {
        var orders = await _context.Orders.AsNoTracking()
                                          .Include(o => o.OrderProducts)
                                          .ThenInclude(op => op.Product)
                                          .Where(o => o.paid == false)
                                          .Select(o => new OrderDto
                                          {
                                              Id = o.Id,
                                              Client = new ClientDto
                                              {
                                                  Id = o.Client.Id,
                                                  Table = o.Client.Table.Number.ToString(),
                                                  Name = o.Client.Name
                                              },
                                              CreatedAt = o.CreatedAt,
                                              UpdatedAt = o.UpdatedAt,
                                              OrderProducts = o.OrderProducts
                                                               .Where(op => op.IsFinish == false)
                                                               .Select(op => new OrderProductDto
                                                               {
                                                                   id = op.Id,
                                                                   ProductId = op.ProductId,
                                                                   IsFinish = op.IsFinish,
                                                                   Product = new ProductDto
                                                                   {
                                                                       Id = op.Product.Id,
                                                                       Name = op.Product.Name,
                                                                       Price = op.Product.Price
                                                                   }
                                                               }).ToList()
                                          })
                                          .ToListAsync();

        return orders;
    }

    public async Task<ICollection<OrderDto>> GetAllOrdersKitchen()
    {
        var orders = await _context.Orders.AsNoTracking()
                                          .Where(o => o.paid == false && o.OrderProducts.Any(op => op.IsFinish == false))
                                          .Select(o => new OrderDto
                                          {
                                              Id = o.Id,
                                              Client = new ClientDto
                                              {
                                                  Id = o.Client.Id,
                                                  Table = o.Client.Table.Number.ToString(),
                                                  Name = o.Client.Name
                                              },
                                              CreatedAt = o.CreatedAt,
                                              UpdatedAt = o.UpdatedAt,
                                              OrderProducts = o.OrderProducts
                                                               .Where(op => op.IsFinish == false)
                                                               .Select(op => new OrderProductDto
                                                               {
                                                                   id = op.Id,
                                                                   ProductId = op.ProductId,
                                                                   IsFinish = op.IsFinish,
                                                                   Product = new ProductDto
                                                                   {
                                                                       Id = op.Product.Id,
                                                                       Name = op.Product.Name,
                                                                       Price = op.Product.Price
                                                                   }
                                                               }).ToList()
                                          })
                                          .ToListAsync();

        return orders;
    }


    public async Task FinishDish(int orderProductId)
    {
        var order = await _context.Orders
                                  .Include(o => o.OrderProducts) 
                                  .FirstOrDefaultAsync(o => o.OrderProducts.Any(op => op.Id == orderProductId));

        if (order == null)
        {
            throw new KeyNotFoundException("Pedido não encontrado.");
        }

        var orderProduct = order.OrderProducts.FirstOrDefault(op => op.Id == orderProductId);

        if (orderProduct == null)
        {
            throw new KeyNotFoundException("Produto não encontrado no pedido.");
        }

        orderProduct.IsFinish = !orderProduct.IsFinish;

        await _context.SaveChangesAsync();
    }

    public async Task<SelectOrderDto> GetOrderByClientId(Guid id) 
    {
        return _mapper.Map<SelectOrderDto>(await _context.Orders.FirstOrDefaultAsync(o => o.ClientId == id && o.Client.Active == true));
    }
}
