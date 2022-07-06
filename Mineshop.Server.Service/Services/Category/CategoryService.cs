using AutoMapper;
using Infrastructure.Repositories.Interfaces.Category;
using Infrastructure.Repositories.Interfaces.Server;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains.Category;
using Mineshop.Server.Model.Models.Category;
using Mineshop.Server.Service.Services.Interfaces.Category;

namespace Mineshop.Server.Service.Services.Category;

public class CategoryService :
    MineshopService<CategoryViewModel, CategoryEntity>,
    ICategoryService
{
    private readonly IMapper _mapper;

    private readonly ICategoryRepository _repository;

    private readonly IServerRepository _serverRepository;

    public CategoryService(
        IMapper mapper,
        ICategoryRepository repository,
        IServerRepository serverRepository
    ) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
        _serverRepository = serverRepository;
    }

    public override async Task<CategoryViewModel> Create(CategoryViewModel viewModel)
    {
        AssertIfServerExists(viewModel.ServerIdentifier);

        var entity = _mapper.Map<CategoryEntity>(viewModel);
        return _mapper.Map<CategoryViewModel>(await _repository.Create(entity));
    }

    public override async Task<CategoryViewModel> Update(CategoryViewModel viewModel)
    {
        AssertIfServerExists(viewModel.ServerIdentifier);

        var entityByIdentifier = await _repository.GetByIdentifier(viewModel.Identifier);
        if (entityByIdentifier == null) throw new ArgumentException("Category with this identifier does not exist");

        return _mapper.Map<CategoryViewModel>(await _repository.Update(entityByIdentifier));
    }

    public async Task<List<CategoryViewModel>> SearchAll(SearchCategoryViewModel search)
    {
        var queryable = _repository.Queryable()
            .AsNoTracking();

        if (search.Name != null)
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
        }

        if (search.Description != null)
        {
            queryable = queryable
                .Where(x =>
                    !string.IsNullOrEmpty(x.Description)
                    && x.Description.ToLower().Contains(search.Description.ToLower())
                );
        }

        return _mapper.Map<List<CategoryViewModel>>(await queryable.ToListAsync());
    }

    public async Task<CategoryViewModel?> GetByName(string name)
    {
        return _mapper.Map<CategoryViewModel>(await _repository.GetByName(name));
    }

    private async void AssertIfServerExists(Guid serverIdentifier)
    {
        if (await _serverRepository.GetByIdentifier(serverIdentifier) == null)
            throw new ArgumentException("Server with this identifier does not exist");
    }
}