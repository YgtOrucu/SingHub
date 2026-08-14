using FluentValidation;
using SingHub.Application.Features.Auths.Queries;

namespace SingHub.Application.Features.Auths.Validators;

public class LoginUserValidator : AbstractValidator<GetLoginQuery>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }
}
