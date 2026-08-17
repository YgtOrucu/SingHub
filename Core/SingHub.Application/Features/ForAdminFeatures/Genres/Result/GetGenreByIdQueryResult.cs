using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Result;
public class GetGenreByIdQueryResult : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
