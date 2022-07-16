using Mineshop.Server.Payment.Models.Interfaces;

namespace Mineshop.Server.Payment.Models;

public sealed class SandboxCreatedPayment : ICreatedPayment
{
    public SandboxCreatedPayment(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; }
    public string? Redirect { get; }
}

public sealed class StripeCreatedPayment : ICreatedPayment
{
    public StripeCreatedPayment(string identifier, string redirect)
    {
        Identifier = identifier;
        Redirect = redirect;
    }

    public string Identifier { get; }
    public string? Redirect { get; }
}