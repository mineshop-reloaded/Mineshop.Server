using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Mineshop.Server.Domain.Entity;
using Mineshop.Server.Model.Models;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Service.Services;

public abstract class MineshopService<T, TE> : IMineshopService<T>
    where T : MineshopModel
    where TE : MineshopEntity
{
    private readonly IMapper _mapper;

    private readonly IMineshopRepository<TE> _repository;

    protected MineshopService(IMapper mapper, IMineshopRepository<TE> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public virtual async Task<T?> GetByIdentifier(Guid identifier)
    {
        var mineshopEntity = await _repository.GetByIdentifier(identifier);
        return _mapper.Map<T>(mineshopEntity);
    }

    public virtual async Task<List<T>> GetAll()
    {
        var mineshopEntities = await _repository.GetAll();
        return _mapper.Map<List<T>>(mineshopEntities);
    }

    public virtual async Task Delete(Guid identifier)
    {
        await _repository.Delete(identifier);
    }

    public abstract Task<T> Create(T model);
    public abstract Task<T> Update(T model);
}