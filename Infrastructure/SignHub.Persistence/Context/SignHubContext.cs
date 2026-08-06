using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignHub.Domain.Entities;

namespace SignHub.Persistence.Context;

public class SignHubContext(DbContextOptions<SignHubContext> dbContext) : IdentityDbContext<AppUser, AppRole, Guid>(dbContext)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(SignHubContext).Assembly);
    }
}
