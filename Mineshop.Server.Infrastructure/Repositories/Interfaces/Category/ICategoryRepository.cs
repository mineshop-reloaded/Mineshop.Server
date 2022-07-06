using Mineshop.Server.Domain.Domains.Category;

namespace Infrastructure.Repositories.Interfaces.Category;

public interface ICategoryRepository : IMineshopRepository<CategoryEntity>
{
    Task<CategoryEntity?> GetByName(string name);
}