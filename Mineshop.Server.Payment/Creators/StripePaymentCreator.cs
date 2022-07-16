using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Payment.Creators.Interfaces;
using Mineshop.Server.Payment.Models;
using Mineshop.Server.Payment.Models.Interfaces;
using Stripe.Checkout;

namespace Mineshop.Server.Payment.Creators;

public class StripePaymentCreator : IPaymentCreator
{
    private readonly SessionService _sessionService;

    public StripePaymentCreator()
    {
        _sessionService = new SessionService();
    }

    public ICreatedPayment Create(PaymentEntity paymentEntity)
    {
        Console.WriteLine(paymentEntity.Products[0]);
        var checkoutProducts = paymentEntity.Products.Select(
            x => new SessionLineItemOptions
            {
                Quantity = x.Quantity,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "BRL",
                    UnitAmountDecimal = x.Price,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = x.Product.Name,
                        Description = x.Product.Description ?? "No description",
                    },
                },
            }
        );

        var sessionCreateOptions = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            Mode = "payment",
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    {
                        "mineshop_identifier", paymentEntity.Identifier.ToString()
                    },
                },
            },
            LineItems = checkoutProducts.ToList(),
        };

        var checkout = _sessionService.Create(sessionCreateOptions);
        return new StripeCreatedPayment(
            checkout.Id,
            checkout.Url
        );
    }
}