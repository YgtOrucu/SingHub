namespace SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Result;

public class ArtistDetailsAndSongQueryResult
{
    public ArtistDto ArtistDto { get; set; }
    public IList<SongDto> SongDtos { get; set; }
}
