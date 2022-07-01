using Mineshop.Server.Domain.Domains;
using FluentValidation;

namespace Mineshop.Server.Domain.Validators;

public class ServerValidator : AbstractValidator<Domains.ServerEntity>
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