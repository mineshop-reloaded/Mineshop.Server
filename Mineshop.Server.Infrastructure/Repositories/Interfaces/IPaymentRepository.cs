using Mineshop.Server.Domain.Domains;

namespace Infrastructure.Repositories.Interfaces;

public interface IPaymentRepository : IMineshopRepository<PaymentEntity>
{
}

public interface IPaymentProductRepository : IMineshopRepository<PaymentProductEntity>
{
}