using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories;

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