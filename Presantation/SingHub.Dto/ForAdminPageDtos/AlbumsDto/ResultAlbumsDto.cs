using SingHub.Dto.Base;

namespace SingHub.Dto.ForAdminPageDtos.AlbumsDto;

public class ResultAlbumsDto : AuditableEntity
{
    public string Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public string ArtistName { get; set; }
    public int SongCountByAlbum { get; set; }
}
