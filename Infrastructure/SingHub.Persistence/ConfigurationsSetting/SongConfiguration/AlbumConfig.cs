using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.ConfigurationsSetting.SongConfiguration;

public class AlbumConfig : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasColumnType("nvarchar(150)").IsRequired();
        builder.Property(x => x.CoverImageUrl).HasColumnType("nvarchar(700)").IsRequired(false);

        builder.HasOne(x => x.Artist).WithMany(x => x.Albums).HasForeignKey(x => x.ArtistId).OnDelete(DeleteBehavior.Restrict);
    }
}
