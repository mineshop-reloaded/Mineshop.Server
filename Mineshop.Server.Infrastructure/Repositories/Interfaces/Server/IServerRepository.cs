using Mineshop.Server.Domain.Domains.Server;

namespace Infrastructure.Repositories.Interfaces.Server;

public interface IServerRepository : IMineshopRepository<ServerEntity>
{
    Task<ServerEntity?> GetByName(string name);
}