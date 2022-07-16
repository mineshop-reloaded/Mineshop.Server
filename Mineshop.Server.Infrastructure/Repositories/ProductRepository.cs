using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories;

public class ProductRepository : MineshopRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(MineshopContext context) : base(context)
    {
    }
}