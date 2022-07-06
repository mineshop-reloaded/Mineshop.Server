using Mineshop.Server.Model.Models.Server;

namespace Mineshop.Server.Service.Services.Interfaces.Server;

public interface IServerService : IMineshopService<ServerViewModel>
{
    Task<List<ServerViewModel>> SearchAll(SearchServerViewModel search);
    Task<ServerViewModel?> GetByName(string name);
}