using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Context;

public class MineshopContext : DbContext
{
    public virtual DbSet<ServerEntity> Servers { get; set; }
    public virtual DbSet<CategoryEntity> Categories { get; set; }
    public virtual DbSet<ProductEntity> Products { get; set; }
    public virtual DbSet<PaymentEntity> Payments { get; set; }
    public virtual DbSet<PaymentProductEntity> PaymentProducts { get; set; }

    public MineshopContext(DbContextOptions options) : base(options)
    {
    }
}