using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Commands;

public class CreateArtistCommand : IRequest<BaseResult<object>>
{
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
}
