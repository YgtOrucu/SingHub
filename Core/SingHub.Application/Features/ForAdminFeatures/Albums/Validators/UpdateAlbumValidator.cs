using FluentValidation;
using SingHub.Application.Features.ForAdminFeatures.Albums.Commands;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Validators;

public class UpdateAlbumValidator : AbstractValidator<UpdateAlbumCommand>
{
    public UpdateAlbumValidator()
    {
        RuleFor(x => x.Title).AlbumTitle();
        RuleFor(x => x.CoverImageUrl).AlbumCoverImageUrl();
        RuleFor(x => x.ReleaseDate).AlbumReleaseDate();
    }
}
