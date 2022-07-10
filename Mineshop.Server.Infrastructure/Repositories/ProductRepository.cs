using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories;

public class ProductRepository : MineshopRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(MineshopContext context) : base(context)
    {
    }

    public async Task<ProductEntity?> GetByName(string name)
    {
        return await Context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}