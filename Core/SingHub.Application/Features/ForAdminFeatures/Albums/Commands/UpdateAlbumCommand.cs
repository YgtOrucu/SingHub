using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Commands;

public class UpdateAlbumCommand : IRequest<BaseResult<object>>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int ArtistId { get; set; }
}
