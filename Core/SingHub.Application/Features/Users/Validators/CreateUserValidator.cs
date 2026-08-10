using FluentValidation;
using SingHub.Application.Features.Users.Commands;

namespace SingHub.Application.Features.Users.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("FirstName is required")
            .MinimumLength(3).WithMessage("The FirstName must be at least 3 characters long.")
            .MaximumLength(35).WithMessage("The FirstName must be at most 35 characters long.");

        RuleFor(x => x.Surname).NotEmpty().WithMessage("LastName is required")
            .MinimumLength(3).WithMessage("The LastName must be at least 3 characters long.")
            .MaximumLength(35).WithMessage("The LastName must be at most 35 characters long.");
    }
}
