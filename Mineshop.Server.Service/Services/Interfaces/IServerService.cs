using Mineshop.Server.Model.Models;
using Mineshop.Server.Model.Models.Server;

namespace Mineshop.Server.Service.Services.Interfaces;

public interface IServerService : IMineshopService<ServerViewModel>
{
    Task<ServerViewModel?> GetByName(string name);
}