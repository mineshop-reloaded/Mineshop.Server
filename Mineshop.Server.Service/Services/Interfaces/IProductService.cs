using Mineshop.Server.Model.Models.Product;

namespace Mineshop.Server.Service.Services.Interfaces;

public interface IProductService : IMineshopService<ProductViewModel>
{
    Task<List<ProductViewModel>> SearchAll(SearchProductViewModel search);
}