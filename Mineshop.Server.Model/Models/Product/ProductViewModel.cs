namespace Mineshop.Server.Model.Models.Product;

public class ProductViewModel : MineshopModel
{
    public override Guid Identifier { get; set; }
    public Guid CategoryIdentifier { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}