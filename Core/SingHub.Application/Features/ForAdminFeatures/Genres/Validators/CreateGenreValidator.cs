using FluentValidation;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Validators;

public class CreateGenreValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
            .MaximumLength(80).WithMessage("Name cannot exceed 80 characters.")
            .Matches(@"^[a-zA-ZğĞıİöÖüÜşŞçÇ\s]+$").WithMessage("Name must contain only letters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
            .MaximumLength(80).WithMessage("Name cannot exceed 80 characters.")
            .Matches(@"^[a-zA-ZğĞıİöÖüÜşŞçÇ\s]+$").WithMessage("Name must contain only letters.");
    }
}
