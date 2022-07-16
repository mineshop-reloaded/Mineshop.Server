using Mineshop.Server.Model.Models.Payment;
using Mineshop.Server.Payment.Models.Interfaces;

namespace Mineshop.Server.Payment.Models;

public class CreatedPaymentViewModel
{
    public PaymentViewModel Payment { get; }

    public ICreatedPayment Reference { get; }

    public CreatedPaymentViewModel(PaymentViewModel payment, ICreatedPayment reference)
    {
        Payment = payment;
        Reference = reference;
    }
}