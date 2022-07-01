using Mineshop.Server.Model.Models.Interfaces;

namespace Mineshop.Server.Model.Models;

public abstract class MineshopModel : IMineshopModel
{
    public abstract Guid Identifier { get; set; }
}