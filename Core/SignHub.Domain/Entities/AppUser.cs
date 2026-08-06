using Microsoft.AspNetCore.Identity;

namespace SignHub.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string AvatarUrl { get; set; }
}
