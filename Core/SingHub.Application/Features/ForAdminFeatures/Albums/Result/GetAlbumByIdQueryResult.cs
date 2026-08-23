using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Result;

public class GetAlbumByIdQueryResult : BaseDto
{
    public string Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
}
