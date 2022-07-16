using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Model.Models.Category;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Service.Services;

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
        await _serverRepository.GetAndAssertByIdentifier(viewModel.ServerIdentifier);

        var entity = _mapper.Map<CategoryEntity>(viewModel);
        return _mapper.Map<CategoryViewModel>(await _repository.Create(entity));
    }

    public override async Task<CategoryViewModel> Update(CategoryViewModel viewModel)
    {
        await _serverRepository.GetAndAssertByIdentifier(viewModel.ServerIdentifier);

        var entityByIdentifier = await _repository.GetAndAssertByIdentifier(viewModel.Identifier);
        return _mapper.Map<CategoryViewModel>(await _repository.Update(entityByIdentifier));
    }

    public async Task<List<CategoryViewModel>> SearchAll(SearchCategoryViewModel search)
    {
        var queryable = _repository.Queryable()
            .AsNoTracking();

        if (search.ServerIdentifier != null)
        {
            queryable = queryable.Where(x => x.ServerIdentifier == search.ServerIdentifier);
        }

        if (!string.IsNullOrEmpty(search.Name))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
        }

        if (!string.IsNullOrEmpty(search.Description))
        {
            queryable = queryable
                .Where(x =>
                    !string.IsNullOrEmpty(x.Description)
                    && x.Description.ToLower().Contains(search.Description.ToLower())
                );
        }

        return _mapper.Map<List<CategoryViewModel>>(await queryable.ToListAsync());
    }
}