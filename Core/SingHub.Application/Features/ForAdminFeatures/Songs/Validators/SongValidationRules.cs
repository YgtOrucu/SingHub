using FluentValidation;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Validators;

public static class SongValidationRules
{
    public static IRuleBuilderOptions<T, string> SongTitle<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Song title is required.")
            .NotNull().WithMessage("Song title cannot be null.")
            .MinimumLength(2).WithMessage("Song title must be at least 2 characters long.")
            .MaximumLength(150).WithMessage("Song title must not exceed 150 characters.");
    }

    public static IRuleBuilderOptions<T, string> SongAudioUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Audio URL is required.")
            .MaximumLength(1000).WithMessage("Audio URL must not exceed 1000 characters.");
    }

    public static IRuleBuilderOptions<T, string?> SongCoverImageUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(1000).WithMessage("Cover image URL must not exceed 1000 characters.")
            .Must(x => string.IsNullOrWhiteSpace(x) || BeValidUrl(x))
            .WithMessage("Please enter a valid URL format (e.g., https://domain.com/image.jpg).");
    }

    public static IRuleBuilderOptions<T, DateTime> SongReleaseDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Release date is required.")
            .LessThanOrEqualTo(DateTime.Now.AddYears(1))
            .WithMessage("Release date cannot be set too far into the future.");
    }

    private static bool BeValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}