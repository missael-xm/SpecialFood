using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SpecialFood.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<SpecialFood.Models.Producto> DataProductos { get; set; }
    public DbSet<SpecialFood.Models.Proforma> DataProforma { get; set; }
    public DbSet<SpecialFood.Models.Pago> DataPago { get; set; }
    public DbSet<SpecialFood.Models.Pedido> DataPedido { get; set; }
    public DbSet<SpecialFood.Models.DetallePedido> DataDetallePedido { get; set; }
}
