using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Commands;

public class UpdateArtistCommand : IRequest<BaseResult<object>>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
}
