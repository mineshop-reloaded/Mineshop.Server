using Mineshop.Server.Model.Models.Payment;
using Mineshop.Server.Payment.Models;

namespace Mineshop.Server.Service.Services.Interfaces;

public interface IPaymentService : IMineshopService<PaymentViewModel>
{
    Task<CreatedPaymentViewModel> CreateWithGateway(PaymentViewModel viewModel);
    Task<List<PaymentViewModel>> SearchAll(SearchPaymentViewModel search);
}