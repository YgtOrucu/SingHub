using SingHub.Domain.Bases;

namespace SingHub.Domain.Entities;
public class Genre : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Song> Songs { get; set; } = new List<Song>();
}