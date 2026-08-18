using FluentValidation;
using SingHub.Application.Features.ForAdminFeatures.Artists.Commands;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Validators;

public class UpdateArtistValidator : AbstractValidator<UpdateArtistCommand>
{
    public UpdateArtistValidator()
    {
        RuleFor(x => x.Name).ArtistName();
        RuleFor(x => x.Biography).Biography();
        RuleFor(x => x.ImageUrl).ImageUrl();
        RuleFor(x => x.BannerUrl).BannerUrl();
        RuleFor(x => x.Country).Country();
        RuleFor(x => x.BirthDate).BirthDate();
    }
}
