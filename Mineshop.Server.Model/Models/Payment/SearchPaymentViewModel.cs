using Mineshop.Server.Domain.Domains;

namespace Mineshop.Server.Model.Models.Payment;

public class SearchPaymentViewModel
{
    public string? MinecraftPlayer { get; set; }
    public PaymentGateway? PaymentGateway { get; set; }
    public PaymentStatus? PaymentStatus { get; set; }
}