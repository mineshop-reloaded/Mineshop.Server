using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories;

public class ServerRepository : MineshopRepository<ServerEntity>, IServerRepository
{
    public ServerRepository(MineshopContext context) : base(context)
    {
    }
}