using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Context;

public class MineshopContext : DbContext
{
    public virtual DbSet<ServerEntity> Servers { get; set; }
    public virtual DbSet<CategoryEntity> Categories { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }

    public MineshopContext(DbContextOptions options) : base(options)
    {
    }
}