using Infrastructure.Repositories.Interfaces;
using Mineshop.Server.Model.Models.Payment.Callback;
using Mineshop.Server.Payment.Handlers.Interfaces;

namespace Mineshop.Server.Payment.Handlers;

public class SandboxPaymentHandler : IPaymentHandler
{
    private readonly IPaymentRepository _paymentRepository;

    public SandboxPaymentHandler(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public void Handle(string responseBody)
    {
        throw new NotImplementedException();
    }

    public async Task Handle(PostSandboxPaymentRequestViewModel request)
    {
        var paymentEntity = await _paymentRepository.GetAndAssertByIdentifier(request.PaymentIdentifier);
        if (paymentEntity.PaymentStatus != request.PaymentStatus)
        {
            paymentEntity.PaymentStatus = request.PaymentStatus;
            await _paymentRepository.Update(paymentEntity);
        }
    }
}