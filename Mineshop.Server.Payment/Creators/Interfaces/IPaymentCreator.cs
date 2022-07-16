using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Payment.Models.Interfaces;

namespace Mineshop.Server.Payment.Creators.Interfaces;

public interface IPaymentCreator
{
    ICreatedPayment Create(PaymentEntity paymentEntity);
}