using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Payment.Creators.Interfaces;
using Mineshop.Server.Payment.Models;
using Mineshop.Server.Payment.Models.Interfaces;

namespace Mineshop.Server.Payment.Creators;

public class SandboxPaymentCreator : IPaymentCreator
{
    public ICreatedPayment Create(PaymentEntity paymentEntity)
    {
        return new SandboxCreatedPayment(paymentEntity.Identifier.ToString());
    }
}