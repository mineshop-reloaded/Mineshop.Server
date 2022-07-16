using AutoMapper;
using Mineshop.Server.Domain.Domains;
using Mineshop.Server.Model.Models.Payment;

namespace Mineshop.Server.Application.Mappers;

public class PaymentMapper : Profile
{
    public PaymentMapper()
    {
        CreateMap<PaymentEntity, PaymentViewModel>()
            .ReverseMap();
        CreateMap<PostPaymentRequestViewModel, PaymentViewModel>();

        CreateMap<PaymentProductEntity, PaymentProductViewModel>()
            .ReverseMap();
        CreateMap<PostPaymentProductRequestViewModel, PaymentProductViewModel>();
    }
}