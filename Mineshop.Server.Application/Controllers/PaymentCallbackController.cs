using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mineshop.Server.Application.Options;
using Mineshop.Server.Model.Models.Payment.Callback;
using Mineshop.Server.Payment.Handlers;
using Stripe;

namespace Mineshop.Server.Application.Controllers;

[Route("api/payment/callback")]
[ApiController]
public class PaymentCallbackController : ControllerBase
{
    [HttpPost("sandbox")]
    public async Task<IActionResult> SandboxCallback(
        [FromBody]
        PostSandboxPaymentRequestViewModel viewModel,
        [FromServices]
        SandboxPaymentHandler paymentHandler
    )
    {
        await paymentHandler.Handle(viewModel);
        return Ok();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("stripe")]
    public async Task<IActionResult> StripeCallback(
        [FromServices]
        IOptions<StripeOptions> stripeOptions,
        [FromServices]
        StripePaymentHandler paymentHandler
    )
    {
        var requestBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        EventUtility.ValidateSignature(
            requestBody,
            Request.Headers["Stripe-Signature"],
            stripeOptions.Value.WebhookSigningKey
        );

        paymentHandler.Handle(requestBody);
        return Ok();
    }
}