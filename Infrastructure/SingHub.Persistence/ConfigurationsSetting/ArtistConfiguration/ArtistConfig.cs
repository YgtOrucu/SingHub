using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.ConfigurationsSetting.ArtistConfiguration;

public class ArtistConfig : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("nvarchar(80)").IsRequired();
        builder.Property(x => x.Biography).HasColumnType("nvarchar(2000)").IsRequired(false);
        builder.Property(x => x.ImageUrl).HasColumnType("nvarchar(700)").IsRequired(false);
        builder.Property(x => x.BannerUrl).HasColumnType("nvarchar(700)").IsRequired(false);
        builder.Property(x => x.Country).HasColumnType("nvarchar(50)").IsRequired(false);
    }
}
