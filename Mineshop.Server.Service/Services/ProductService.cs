using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Model.Models.Product;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Service.Services;

public class ProductService :
    MineshopService<ProductViewModel, ProductEntity>,
    IProductService
{
    private readonly IMapper _mapper;

    private readonly IProductRepository _repository;

    private readonly ICategoryRepository _categoryRepository;

    public ProductService(
        IMapper mapper,
        IProductRepository repository,
        ICategoryRepository categoryRepository
    ) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public override async Task<ProductViewModel> Create(ProductViewModel viewModel)
    {
        await _categoryRepository.GetAndAssertByIdentifier(viewModel.CategoryIdentifier);

        var entity = _mapper.Map<ProductEntity>(viewModel);
        return _mapper.Map<ProductViewModel>(await _repository.Create(entity));
    }

    public override async Task<ProductViewModel> Update(ProductViewModel viewModel)
    {
        await _categoryRepository.GetAndAssertByIdentifier(viewModel.CategoryIdentifier);

        var entityByIdentifier = await _repository.GetAndAssertByIdentifier(viewModel.Identifier);
        return _mapper.Map<ProductViewModel>(await _repository.Update(entityByIdentifier));
    }

    public async Task<List<ProductViewModel>> SearchAll(SearchProductViewModel search)
    {
        var queryable = _repository.Queryable()
            .AsNoTracking();

        if (search.CategoryIdentifier != null)
        {
            queryable = queryable.Where(x => x.CategoryIdentifier == search.CategoryIdentifier);
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

        return _mapper.Map<List<ProductViewModel>>(await queryable.ToListAsync());
    }

    public async Task<ProductViewModel?> GetByName(string name)
    {
        return _mapper.Map<ProductViewModel>(await _repository.GetByName(name));
    }
}