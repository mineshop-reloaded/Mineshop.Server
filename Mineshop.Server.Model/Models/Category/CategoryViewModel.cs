namespace Mineshop.Server.Model.Models.Category;

public class CategoryViewModel : MineshopModel
{
    public override Guid Identifier { get; set; }
    public Guid ServerIdentifier { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}