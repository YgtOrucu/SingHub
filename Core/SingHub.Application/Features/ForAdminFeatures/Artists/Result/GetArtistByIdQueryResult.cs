using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Result;
public class GetArtistByIdQueryResult : BaseDto
{
    public string Name { get; set; }
    public string Biography { get; set; }
    public string ImageUrl { get; set; }
    public string BannerUrl { get; set; }
    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
}
