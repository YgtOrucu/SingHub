using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Result;

public class GetGenreQueryResult : AuditableDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int SongByGenreCount { get; set; }
}
