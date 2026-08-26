using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.Context;

public class SingHubContext(DbContextOptions<SingHubContext> dbContext) : IdentityDbContext<AppUser, AppRole, Guid>(dbContext)
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<SongAppRole> SongAppRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(SingHubContext).Assembly);
    }
}
