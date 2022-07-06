using AutoMapper;
using Mineshop.Server.Domain.Domains.Server;
using Mineshop.Server.Model.Models.Server;

namespace Mineshop.Server.Application.Mappers;

public class ServerMapper : Profile
{
    public ServerMapper()
    {
        CreateMap<ServerEntity, ServerViewModel>()
            .ReverseMap();
        CreateMap<PostServerRequestViewModel, ServerViewModel>();
    }
}