using System.ComponentModel.DataAnnotations;
using Mineshop.Server.Domain.Entity;

namespace Mineshop.Server.Domain.Domains;

public class ServerEntity : MineshopEntity
{
    [Required]
    [StringLength(32)]
    public string Name { get; set; }
}