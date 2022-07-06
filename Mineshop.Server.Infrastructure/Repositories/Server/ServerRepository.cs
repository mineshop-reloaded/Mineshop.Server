using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces.Server;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains.Server;

namespace Infrastructure.Repositories.Server;

public class ServerRepository : MineshopRepository<ServerEntity>, IServerRepository
{
    public ServerRepository(MineshopContext context) : base(context)
    {
    }

    public async Task<ServerEntity?> GetByName(string name)
    {
        return await Context.Servers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}