namespace SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Result;
public class ArtistDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? BannerUrl { get; set; }
    public string? Country { get; set; }
    public DateTime? BirthDate { get; set; }
}
