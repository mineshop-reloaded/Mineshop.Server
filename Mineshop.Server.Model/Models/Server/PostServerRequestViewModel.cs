using System.ComponentModel.DataAnnotations;

namespace Mineshop.Server.Model.Models.Server;

public class PostServerRequestViewModel
{
    [Required]
    [StringLength(32, MinimumLength = 3)]
    public string Name { get; set; }
}