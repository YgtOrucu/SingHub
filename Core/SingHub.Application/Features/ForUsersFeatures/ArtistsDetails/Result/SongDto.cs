namespace SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Result;

public class SongDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Duration { get; set; }
    public string AudioUrl { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
}
