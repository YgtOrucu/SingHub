using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.ConfigurationsSetting.AppUserConfiguration;

public class AppUserConfig : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired(true);
        builder.Property(x => x.Surname).HasColumnType("varchar(50)").IsRequired(true);
        builder.Property(x => x.AvatarUrl).HasColumnType("varchar(250)").IsRequired(false);



        builder.Property(x => x.UserName).HasColumnType("varchar(80)");
        builder.Property(x => x.Email).HasColumnType("varchar(50)");
        builder.Property(x => x.NormalizedEmail).HasColumnType("varchar(50)");
        builder.Property(x => x.NormalizedUserName).HasColumnType("varchar(50)");
        builder.Property(x => x.PhoneNumber).HasColumnType("varchar(50)");
    }
}
