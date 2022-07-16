using AutoMapper;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Model.Models.Product;

namespace Mineshop.Server.Application.Mappers;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductEntity, ProductViewModel>()
            .ReverseMap();
        CreateMap<PostProductRequestViewModel, ProductViewModel>();
    }
}