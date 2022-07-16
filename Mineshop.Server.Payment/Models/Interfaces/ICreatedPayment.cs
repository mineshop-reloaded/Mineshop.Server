namespace Mineshop.Server.Payment.Models.Interfaces;

public interface ICreatedPayment
{
    string Identifier { get; }
    public string? Redirect { get; }
}