using FluentValidation;
using Mineshop.Server.Domain.Domains.Server;

namespace Mineshop.Server.Domain.Validators;

public class ServerValidator : AbstractValidator<ServerEntity>
{
    public ServerValidator()
    {
        RuleFor(x => x.Identifier)
            .NotNull();

        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 32);
    }
}