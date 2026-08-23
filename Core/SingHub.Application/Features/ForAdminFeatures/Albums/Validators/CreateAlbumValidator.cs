using FluentValidation;
using SingHub.Application.Features.ForAdminFeatures.Albums.Commands;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Validators;

public class CreateAlbumValidator : AbstractValidator<CreateAlbumCommand>
{
    public CreateAlbumValidator()
    {
        RuleFor(x => x.Title).AlbumTitle();
        RuleFor(x => x.CoverImageUrl).AlbumCoverImageUrl();
        RuleFor(x => x.ReleaseDate).AlbumReleaseDate();
    }
}
