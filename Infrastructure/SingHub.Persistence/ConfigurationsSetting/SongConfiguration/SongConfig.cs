using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.ConfigurationsSetting.SongConfiguration;

public class SongConfig : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasColumnType("nvarchar(150)").IsRequired();
        builder.Property(x => x.AudioUrl).HasColumnType("nvarchar(700)").IsRequired();
        builder.Property(x => x.CoverImageUrl).HasColumnType("nvarchar(700)").IsRequired(false);

        builder.HasOne(x => x.Artist).WithMany(x => x.Songs).HasForeignKey(x => x.ArtistId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Genre).WithMany(x => x.Songs).HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Album).WithMany(x => x.Songs).HasForeignKey(x => x.AlbumId).OnDelete(DeleteBehavior.SetNull);
    }
}
