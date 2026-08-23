using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Result;
public class GetAlbumQueryResult : BaseDto
{
    public string Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int SongCountByAlbum { get; set; }
}
