namespace SingHub.Dto.ForAdminPageDtos.DashboardDto.Top5MostListenedToSongsDto;
public class ResultTop5MostListenedToSongs
{
    public string Title { get; set; } = string.Empty;
    public int ListenCount { get; set; }
    public string ArtistName { get; set; } = string.Empty;
    public string GenreName { get; set; } = string.Empty;
}
