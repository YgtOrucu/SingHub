using SingHub.Domain.Bases;
namespace SingHub.Domain.Entities;
public class Artist : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? Country { get; set; }
    public DateTime? BirthDate { get; set; }

    public ICollection<Song> Songs { get; set; } = new List<Song>();
    public ICollection<Album> Albums { get; set; } = new List<Album>();
}
