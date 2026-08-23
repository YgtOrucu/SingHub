namespace SingHub.Application.Features.ForAdminFeatures.Songs.Result;
public class GetSongByIdQueryResult
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public int ListenCount { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string ArtistName { get; set; } = string.Empty;
    public string GenreName { get; set; } = string.Empty;
    public string? AlbumTitle { get; set; }
    public int ArtistId { get; set; }
    public int GenreId { get; set; }
    public int? AlbumId { get; set; }
}
