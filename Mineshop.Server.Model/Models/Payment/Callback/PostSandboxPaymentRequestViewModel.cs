using System.ComponentModel.DataAnnotations;
using Mineshop.Server.Domain.Domains;

namespace Mineshop.Server.Model.Models.Payment.Callback;

public class PostSandboxPaymentRequestViewModel
{
    [Required]
    public Guid PaymentIdentifier { get; set; }

    [Required]
    public PaymentStatus PaymentStatus { get; set; }
}