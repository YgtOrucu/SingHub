using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.Context;

public class SingHubContext(DbContextOptions<SingHubContext> dbContext) : IdentityDbContext<AppUser, AppRole, Guid>(dbContext)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(SingHubContext).Assembly);
    }
}
