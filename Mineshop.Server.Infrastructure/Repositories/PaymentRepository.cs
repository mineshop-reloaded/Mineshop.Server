using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories;

public class PaymentRepository : MineshopRepository<PaymentEntity>, IPaymentRepository
{
    public PaymentRepository(MineshopContext context) : base(context)
    {
    }
}