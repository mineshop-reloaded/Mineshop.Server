using System.ComponentModel.DataAnnotations;
using Mineshop.Server.Domain.Entity;

namespace Mineshop.Server.Domain.Domains;

public class CategoryEntity : MineshopEntity
{
    [Required]
    public ServerEntity Server { get; set; }

    public Guid ServerIdentifier { get; set; }

    [Required]
    [StringLength(32)]
    public string Name { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }
}