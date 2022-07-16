using System.ComponentModel.DataAnnotations;
using Mineshop.Server.Domain.Domains;

namespace Mineshop.Server.Model.Models.Payment;

public class PostPaymentRequestViewModel
{
    [Required]
    [StringLength(16, MinimumLength = 3)]
    public string MinecraftPlayer { get; set; }

    [Required]
    public PaymentGateway PaymentGateway { get; set; }

    [Required]
    public IList<PostPaymentProductRequestViewModel> Products { get; set; }
}

public class PostPaymentProductRequestViewModel
{
    [Required]
    public string ProductIdentifier { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}