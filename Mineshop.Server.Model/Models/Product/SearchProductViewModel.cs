namespace Mineshop.Server.Model.Models.Product;

public class SearchProductViewModel
{
    public Guid? CategoryIdentifier { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}