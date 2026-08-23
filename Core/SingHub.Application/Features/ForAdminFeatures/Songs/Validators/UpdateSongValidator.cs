using FluentValidation;
using SingHub.Application.Features.ForAdminFeatures.Songs.Commands;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Validators;

public class UpdateSongValidator : AbstractValidator<UpdateSongCommand>
{
    public UpdateSongValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Valid Song ID is required.");
        RuleFor(x => x.Title).SongTitle();
        RuleFor(x => x.AudioUrl).SongAudioUrl();
        RuleFor(x => x.CoverImageUrl).SongCoverImageUrl();
        RuleFor(x => x.ReleaseDate).SongReleaseDate();
        RuleFor(x => x.ArtistId).GreaterThan(0).WithMessage("Artist selection is required.");
        RuleFor(x => x.GenreId).GreaterThan(0).WithMessage("Genre selection is required.");
    }
}