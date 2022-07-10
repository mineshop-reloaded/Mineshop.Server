using System.ComponentModel.DataAnnotations;

namespace Mineshop.Server.Model.Models.Product;

public class PostProductRequestViewModel
{
    [Required]
    public Guid CategoryIdentifier { get; set; }

    [Required]
    [StringLength(32)]
    public string Name { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }

    [Required]
    [Range(0.1, double.MaxValue)]
    public decimal Price { get; set; }
}