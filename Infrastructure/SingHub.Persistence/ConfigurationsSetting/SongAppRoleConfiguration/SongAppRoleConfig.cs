using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.ConfigurationsSetting.SongAppRoleConfiguration;

public class SongAppRoleConfig : IEntityTypeConfiguration<SongAppRole>
{
    public void Configure(EntityTypeBuilder<SongAppRole> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.SongId });

        builder.HasOne(x => x.Song)
            .WithMany(x => x.SongAppRoles)
            .HasForeignKey(x => x.SongId);

        builder.HasOne(x => x.AppRole)
          .WithMany(x => x.SongAppRoles)
          .HasForeignKey(x => x.RoleId);

    }
}
