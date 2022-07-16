namespace Mineshop.Server.Payment.Handlers.Interfaces;

public interface IPaymentHandler
{
    void Handle(string requestBody);
}