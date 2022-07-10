using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Model.Models.Server;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Service.Services;

public class ServerService :
    MineshopService<ServerViewModel, ServerEntity>,
    IServerService
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
        var entity = _mapper.Map<ServerEntity>(viewModel);
        return _mapper.Map<ServerViewModel>(await _repository.Create(entity));
    }

    public override async Task<ServerViewModel> Update(ServerViewModel viewModel)
    {
        var entityByIdentifier = await _repository.GetAndAssertByIdentifier(viewModel.Identifier);
        return _mapper.Map<ServerViewModel>(await _repository.Update(entityByIdentifier));
    }

    public async Task<List<ServerViewModel>> SearchAll(SearchServerViewModel search)
    {
        var queryable = _repository.Queryable()
            .AsNoTracking();

        if (!string.IsNullOrEmpty(search.Name))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
        }

        return _mapper.Map<List<ServerViewModel>>(await queryable.ToListAsync());
    }

    public async Task<ServerViewModel?> GetByName(string name)
    {
        return _mapper.Map<ServerViewModel>(await _repository.GetByName(name));
    }
}