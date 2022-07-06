using System.ComponentModel.DataAnnotations;

namespace Mineshop.Server.Model.Models.Category;

public class PostCategoryRequestViewModel
{
    [Required]
    public Guid ServerIdentifier { get; set; }

    [Required]
    [StringLength(32)]
    public string Name { get; set; }

    public string? Description { get; set; }
}