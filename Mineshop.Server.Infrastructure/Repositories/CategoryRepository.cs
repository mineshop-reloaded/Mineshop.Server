using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories;

public class CategoryRepository : MineshopRepository<CategoryEntity>, ICategoryRepository
{
    public CategoryRepository(MineshopContext context) : base(context)
    {
    }
}