using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories.Interfaces;

public interface IServerRepository : IMineshopRepository<ServerEntity>
{
    Task<ServerEntity?> GetByName(string name);
}