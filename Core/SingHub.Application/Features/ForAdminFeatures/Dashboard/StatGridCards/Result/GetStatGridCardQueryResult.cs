namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Result;
public class GetStatGridCardQueryResult
{ 
    public string MostListenedGenre { get; set; }
    public string MostPopularArtistName { get; set; }
    public string MostPopularArtistListenCount { get; set; }
    public long TotalListenCount { get; set; }
    public string MostListenedSongTitle { get; set; }
    public string MostListenedSongListenCount { get; set; }
    public double PremiumUserPercentage { get; set; }
    public string AverageSongDurationFormatted { get; set; }
    public string AverageSongsPerAlbumFormatted { get; set; }
    public int ActiveCountryCount { get; set; }
    public int ArchivedItemCount { get; set; }
    public int TwoFactorEnabledUserCount { get; set; }
    public int MonthlyAddedSongCount { get; set; }
    public int LockedOutUserCount { get; set; }
}

