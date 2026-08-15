using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SingHub.Domain.Entities;

namespace SingHub.Persistence.ConfigurationsSetting.SongConfiguration;

public class GenreConfig : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("nvarchar(80)").IsRequired();
        builder.Property(x => x.Description).HasColumnType("nvarchar(1000)").IsRequired(false);
        builder.Property(x => x.ImageUrl).HasColumnType("nvarchar(700)").IsRequired(false);
    }
}