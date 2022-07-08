using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories;

public class CategoryRepository : MineshopRepository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(MineshopContext context) : base(context)
    {
    }

    public async Task<CategoryEntity?> GetByName(string name)
    {
        return await Context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}