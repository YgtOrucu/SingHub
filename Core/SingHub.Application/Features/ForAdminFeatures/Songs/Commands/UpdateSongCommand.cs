using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Commands;

public class UpdateSongCommand : IRequest<BaseResult<object>>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
    public int GenreId { get; set; }
    public int? AlbumId { get; set; }
    public List<Guid> SelectedRoleIds { get; set; } = new();
}