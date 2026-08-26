using Microsoft.AspNetCore.Identity;

namespace SingHub.Domain.Entities;

public class AppRole : IdentityRole<Guid>
{
    public ICollection<SongAppRole> SongAppRoles { get; set; } = new List<SongAppRole>();
}
