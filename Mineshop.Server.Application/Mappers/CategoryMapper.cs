using AutoMapper;
using Mineshop.Server.Domain.Domains.Category;
using Mineshop.Server.Model.Models.Category;

namespace Mineshop.Server.Application.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CategoryEntity, CategoryViewModel>()
            .ReverseMap();
        CreateMap<PostCategoryRequestViewModel, CategoryViewModel>();
    }
}