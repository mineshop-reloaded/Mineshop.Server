using Mineshop.Server.Model.Models.Category;

namespace Mineshop.Server.Service.Services.Interfaces;

public interface ICategoryService : IMineshopService<CategoryViewModel>
{
    Task<List<CategoryViewModel>> SearchAll(SearchCategoryViewModel search);
    Task<CategoryViewModel?> GetByName(string name);
}