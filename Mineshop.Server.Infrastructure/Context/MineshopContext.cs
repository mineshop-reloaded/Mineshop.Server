using Mineshop.Server.Domain.Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class MineshopContext : DbContext
{
    public virtual DbSet<ServerEntity> Servers { get; set; }

    public MineshopContext(DbContextOptions options) : base(options)
    {
    }
}