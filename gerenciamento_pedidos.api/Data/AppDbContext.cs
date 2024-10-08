using Microsoft.EntityFrameworkCore;

namespace gerenciamento_pedidos.api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) {}
}
