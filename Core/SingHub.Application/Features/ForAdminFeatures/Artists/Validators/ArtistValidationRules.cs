using FluentValidation;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Validators;

public static class ArtistValidationRules
{
    public static IRuleBuilderOptions<T, string> ArtistName<T>(
         this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty()
            .WithMessage("Artist name is required.")
            .MinimumLength(2)
            .WithMessage("Artist name must be at least 2 characters.")
            .MaximumLength(80)
            .WithMessage("Artist name cannot exceed 80 characters.")
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Artist name cannot consist only of whitespace.");
    }

    public static IRuleBuilderOptions<T, string?> Biography<T>(
        this IRuleBuilder<T, string?> rule)
    {
        return rule
            .MinimumLength(10)
            .WithMessage("Biography must be at least 10 characters.")
            .MaximumLength(2000)
            .WithMessage("Biography cannot exceed 2000 characters.");
    }

    public static IRuleBuilderOptions<T, string?> ImageUrl<T>(
        this IRuleBuilder<T, string?> rule)
    {
        return rule
            .MinimumLength(10)
            .WithMessage("Image URL must be at least 10 characters.")
            .MaximumLength(700)
            .WithMessage("Image URL cannot exceed 700 characters.")
            .Must(x => string.IsNullOrWhiteSpace(x) || BeAValidUrl(x))
            .WithMessage("Image URL must be a valid URL.");
    }

    public static IRuleBuilderOptions<T, string?> BannerUrl<T>(
        this IRuleBuilder<T, string?> rule)
    {
        return rule
            .MinimumLength(10)
            .WithMessage("Banner URL must be at least 10 characters.")
            .MaximumLength(700)
            .WithMessage("Banner URL cannot exceed 700 characters.")
            .Must(x => string.IsNullOrWhiteSpace(x) || BeAValidUrl(x))
            .WithMessage("Banner URL must be a valid URL.");
    }

    public static IRuleBuilderOptions<T, string> Country<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .MinimumLength(2)
            .WithMessage("Country must be at least 2 characters.")
            .MaximumLength(50)
            .WithMessage("Country cannot exceed 50 characters.");
    }

    public static IRuleBuilderOptions<T, DateTime> BirthDate<T>(
        this IRuleBuilder<T, DateTime> rule)
    {
        return rule
            .GreaterThan(new DateTime(1800, 1, 1))
            .WithMessage("Birth date must be later than 1800.")
            .LessThan(DateTime.UtcNow)
            .WithMessage("Birth date cannot be in the future.");
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
               && (uri.Scheme == Uri.UriSchemeHttp ||
                   uri.Scheme == Uri.UriSchemeHttps);
    }
}
