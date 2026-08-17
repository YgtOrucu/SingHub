using FluentValidation;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Validators;

public class UpdateGenreValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required.")
           .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
           .MaximumLength(80).WithMessage("Name cannot exceed 80 characters.")
           .Matches(@"^[a-zA-ZğĞıİöÖüÜşŞçÇ\s]+$").WithMessage("Name must contain only letters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(3).WithMessage("Description must be at least 3 characters.")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.")
            .Matches(@"^[a-zA-ZğĞıİöÖüÜşŞçÇ\s]+$").WithMessage("Description must contain only letters.");
    }
}
