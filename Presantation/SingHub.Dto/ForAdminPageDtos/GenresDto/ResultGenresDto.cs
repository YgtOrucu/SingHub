using SingHub.Dto.Base;

namespace SingHub.Dto.ForAdminPageDtos.GenresDto;

public class ResultGenresDto : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int SongByGenreCount { get; set; }
}
