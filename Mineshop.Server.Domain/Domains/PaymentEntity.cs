using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Mineshop.Server.Domain.Entity;
using Newtonsoft.Json.Converters;

namespace Mineshop.Server.Domain.Domains;

[Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
public enum PaymentGateway
{
    [EnumMember(Value = "SANDBOX")]
    Sandbox,

    [EnumMember(Value = "STRIPE")]
    Stripe,
}

[Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
public enum PaymentStatus
{
    [EnumMember(Value = "PENDING")]
    Pending,

    [EnumMember(Value = "Approved")]
    Approved,

    [EnumMember(Value = "CANCELED")]
    Canceled,

    [EnumMember(Value = "REFOUNDED")]
    Refounded,
}

public class PaymentEntity : MineshopEntity
{
    [Required]
    [StringLength(16, MinimumLength = 3)]
    public string MinecraftPlayer { get; set; }

    [Required]
    public PaymentGateway PaymentGateway { get; set; }

    [Required]
    [DefaultValue(PaymentStatus.Pending)]
    public PaymentStatus PaymentStatus { get; set; }

    public virtual IList<PaymentProductEntity> Products { get; set; }
}

public class PaymentProductEntity : MineshopEntity
{
    [ForeignKey("PaymentEntity")]
    public Guid PaymentIdentifier { get; set; }

    public virtual PaymentEntity Payment { get; set; }

    [ForeignKey("ProductEntity")]
    public Guid ProductIdentifier { get; set; }

    public virtual ProductEntity Product { get; set; }

    [Required]
    [Range(0.1, int.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}