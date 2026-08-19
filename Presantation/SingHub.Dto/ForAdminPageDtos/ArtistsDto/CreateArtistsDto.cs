namespace SingHub.Dto.ForAdminPageDtos.ArtistsDto;

public class CreateArtistsDto
{
    public string Name { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? ImageUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
}
