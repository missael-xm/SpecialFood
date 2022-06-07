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
}
