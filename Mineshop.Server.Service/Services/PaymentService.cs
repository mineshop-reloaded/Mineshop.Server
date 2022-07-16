using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Model.Models.Payment;
using Mineshop.Server.Payment.Creators.Interfaces;
using Mineshop.Server.Payment.Models;
using Mineshop.Server.Service.Services.Interfaces;

namespace Mineshop.Server.Service.Services;

public class PaymentService :
    MineshopService<PaymentViewModel, PaymentEntity>,
    IPaymentService
{
    private readonly IMapper _mapper;

    private readonly IDictionary<PaymentGateway, IPaymentCreator> _paymentCreators;

    private readonly IProductRepository _productRepository;

    private readonly IPaymentRepository _repository;

    public PaymentService(
        IMapper mapper,
        IPaymentRepository repository,
        IProductRepository productRepository,
        IDictionary<PaymentGateway, IPaymentCreator> paymentCreators
    ) : base(mapper, repository)
    {
        _mapper = mapper;
        _repository = repository;
        _productRepository = productRepository;
        _paymentCreators = paymentCreators;
    }

    public override Task<PaymentViewModel> Create(PaymentViewModel viewModel)
    {
        throw new NotImplementedException();
    }

    public override Task<PaymentViewModel> Update(PaymentViewModel viewModel)
    {
        throw new NotImplementedException();
    }

    public async Task<CreatedPaymentViewModel> CreateWithGateway(PaymentViewModel viewModel)
    {
        AssertHasPaymentProducts(viewModel);
        await ValidatePaymentProducts(viewModel);

        var entity = _mapper.Map<PaymentEntity>(viewModel);

        Console.WriteLine(entity.Products[0].ProductIdentifier);
        
        var paymentCreator = _paymentCreators[viewModel.PaymentGateway];
        if (paymentCreator == null)
        {
            throw new InvalidOperationException($"Payment creator for {viewModel.PaymentGateway} is not found");
        }

        var aaa = await _repository.Create(entity);
        Console.WriteLine(aaa.Products[0].ProductIdentifier);
        Console.WriteLine(aaa.Products[0].Product);

        var paymentViewModel = _mapper.Map<PaymentViewModel>(aaa);
        var createdPayment = paymentCreator.Create(entity);

        return new CreatedPaymentViewModel(paymentViewModel, createdPayment);
    }

    public async Task<List<PaymentViewModel>> SearchAll(SearchPaymentViewModel search)
    {
        var queryable = _repository.Queryable()
            .AsNoTracking();

        if (!string.IsNullOrEmpty(search.MinecraftPlayer))
        {
            queryable = queryable.Where(x => x.MinecraftPlayer == search.MinecraftPlayer);
        }

        if (search.PaymentGateway != null)
        {
            queryable = queryable.Where(x => x.PaymentGateway == search.PaymentGateway);
        }

        if (search.PaymentStatus != null)
        {
            queryable = queryable.Where(x => x.PaymentStatus == search.PaymentStatus);
        }

        return _mapper.Map<List<PaymentViewModel>>(await queryable.ToListAsync());
    }

    private static void AssertHasPaymentProducts(PaymentViewModel viewModel)
    {
        if (viewModel.Products.Count == 0)
        {
            throw new ArgumentException("Payment must have at least one product");
        }
    }

    private async Task ValidatePaymentProducts(PaymentViewModel viewModel)
    {
        foreach (var paymentProductViewModel in viewModel.Products)
        {
            var productByIdentifier =
                await _productRepository.GetAndAssertByIdentifier(paymentProductViewModel.ProductIdentifier);
            paymentProductViewModel.Price = productByIdentifier.Price;
        }
    }
}