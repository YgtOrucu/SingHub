using Microsoft.AspNetCore.Identity;

namespace SingHub.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? AvatarUrl { get; set; }
}
