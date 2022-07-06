using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains.Category;
using Mineshop.Server.Domain.Domains.Server;

namespace Infrastructure.Context;

public class MineshopContext : DbContext
{
    public virtual DbSet<ServerEntity> Servers { get; set; }
    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public MineshopContext(DbContextOptions options) : base(options)
    {
    }
}