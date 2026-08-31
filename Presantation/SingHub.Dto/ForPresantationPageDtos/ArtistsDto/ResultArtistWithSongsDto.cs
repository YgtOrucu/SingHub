namespace SingHub.Dto.ForPresantationPageDtos.ArtistsDto;

public class ResultArtistWithSongsDto
{
    public ArtistDto ArtistDto { get; set; }
    public IList<SongDto> SongDtos { get; set; }
}

public class ArtistDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? BannerUrl { get; set; }
    public string? Country { get; set; }
    public DateTime? BirthDate { get; set; }
}

public class SongDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
}
