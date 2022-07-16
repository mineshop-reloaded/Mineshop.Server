using Mineshop.Server.Payment.Handlers.Interfaces;
using Stripe;

namespace Mineshop.Server.Payment.Handlers;

public class StripePaymentHandler : IPaymentHandler
{
    public void Handle(string requestBody)
    {
        var stripeEvent = EventUtility.ParseEvent(requestBody);
        switch (stripeEvent.Type)
        {
            case "payment_intent.succeeded":
            {
                if (stripeEvent.Data.Object is PaymentIntent paymentIntent)
                {
                    Console.WriteLine(paymentIntent.Metadata["mineshop_payment_identifier"]);
                }

                break;
            }
        }
    }
}