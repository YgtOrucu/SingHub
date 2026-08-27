namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Result;

public class Top5MostListenedToSongsQueryResult
{
    public string Title { get; set; } = string.Empty;
    public int ListenCount { get; set; }
    public string ArtistName { get; set; } = string.Empty;
    public string GenreName { get; set; } = string.Empty;
}
