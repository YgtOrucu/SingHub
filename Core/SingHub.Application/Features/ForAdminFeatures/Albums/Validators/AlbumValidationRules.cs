using FluentValidation;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Validators;

public static class AlbumValidationRules
{
    public static IRuleBuilderOptions<T, string> AlbumTitle<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Album title is required.")
            .NotNull().WithMessage("Album title cannot be null.")
            .MinimumLength(5).WithMessage("The album name must be at least 5 characters long.")
            .MaximumLength(150).WithMessage("Album title must not exceed 150 characters.");
    }

    public static IRuleBuilderOptions<T, string?> AlbumCoverImageUrl<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(700).WithMessage("Cover image URL must not exceed 700 characters.")
            .Must(x => string.IsNullOrWhiteSpace(x) || BeAValidUrl(x))
            .WithMessage("Please enter a valid URL format (e.g., https://domain.com/image.jpg).");
    }

    public static IRuleBuilderOptions<T, DateTime> AlbumReleaseDate<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Release date is required.")
            .LessThanOrEqualTo(DateTime.Now.AddYears(1))
            .WithMessage("Release date cannot be set too far into the future.");
    }

    private static bool BeAValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}