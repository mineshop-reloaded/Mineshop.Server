using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories.Interfaces;

public interface ICategoryRepository : IMineshopRepository<CategoryEntity>
{
    Task<CategoryEntity?> GetByName(string name);
}