using SingHub.Domain.Bases;

namespace SingHub.Domain.Entities;

public class Song : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public int ListenCount { get; set; } = 0;
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
    public int? AlbumId { get; set; }
    public Album? Album { get; set; }
}
