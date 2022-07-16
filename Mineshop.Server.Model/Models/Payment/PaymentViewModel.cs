using Mineshop.Server.Domain.Domains;

namespace Mineshop.Server.Model.Models.Payment;

public class PaymentViewModel : MineshopModel
{
    public override Guid Identifier { get; set; }
    public string MinecraftPlayer { get; set; }
    public PaymentGateway PaymentGateway { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public IList<PaymentProductViewModel> Products { get; set; }
}

public class PaymentProductViewModel : MineshopModel
{
    public override Guid Identifier { get; set; }
    public Guid PaymentIdentifier { get; set; }
    public Guid ProductIdentifier { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}