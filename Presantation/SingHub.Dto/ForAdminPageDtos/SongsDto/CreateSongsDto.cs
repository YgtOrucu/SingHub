namespace SingHub.Dto.ForAdminPageDtos.SongsDto;

public class CreateSongsDto
{
    public string Title { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; } = DateTime.Now;
    public int ArtistId { get; set; }
    public int GenreId { get; set; }
    public int? AlbumId { get; set; }
}
