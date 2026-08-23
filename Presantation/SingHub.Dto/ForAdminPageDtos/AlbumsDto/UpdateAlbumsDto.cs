using SingHub.Dto.Base;

namespace SingHub.Dto.ForAdminPageDtos.AlbumsDto;
public class UpdateAlbumsDto : BaseEntity
{
    public string Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
}
