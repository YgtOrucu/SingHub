namespace SingHub.Domain.Entities;
public class SongAppRole
{
    public Guid? RoleId { get; set; }
    public AppRole? AppRole { get; set; }

    public int? SongId { get; set; }
    public Song? Song { get; set; }
}
