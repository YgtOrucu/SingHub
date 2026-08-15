using SingHub.Domain.Bases;
namespace SingHub.Domain.Entities;

public class Album : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
    public ICollection<Song> Songs { get; set; } = new List<Song>();
}
