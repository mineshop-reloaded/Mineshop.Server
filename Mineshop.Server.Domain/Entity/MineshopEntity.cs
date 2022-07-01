using System.ComponentModel.DataAnnotations;

namespace Mineshop.Server.Domain.Entity;

public abstract class MineshopEntity
{
    [Key]
    public Guid Identifier { get; set; }
}