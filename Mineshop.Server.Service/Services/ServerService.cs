using AutoMapper;
using Mineshop.Server.Domain.Domains;
using Infrastructure.Repositories.Interfaces;
using Mineshop.Server.Model.Models;
using Mineshop.Server.Model.Models.Server;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Service.Services;

public class ServerService : MineshopService<ServerViewModel, Domain.Domains.ServerEntity>, IServerService
{
    private readonly IMapper _mapper;

    private readonly IServerRepository _repository;

    public ServerService(IMapper mapper, IServerRepository repository) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public override async Task<ServerViewModel> Create(ServerViewModel viewModel)
    {
        if (await _repository.Contains(x => x.Name == viewModel.Name))
            throw new ArgumentException("Server with this name already exists");

        var server = _mapper.Map<Domain.Domains.ServerEntity>(viewModel);
        return _mapper.Map<ServerViewModel>(await _repository.Create(server));
    }

    public override async Task<ServerViewModel> Update(ServerViewModel viewModel)
    {
        var serverByIdentifier = await _repository.GetByIdentifier(viewModel.Identifier);
        if (serverByIdentifier == null) throw new ArgumentException("Server with this identifier does not exist");

        return _mapper.Map<ServerViewModel>(await _repository.Update(serverByIdentifier));
    }

    public async Task<ServerViewModel?> GetByName(string name)
    {
        return _mapper.Map<ServerViewModel>(await _repository.GetByName(name));
    }
}