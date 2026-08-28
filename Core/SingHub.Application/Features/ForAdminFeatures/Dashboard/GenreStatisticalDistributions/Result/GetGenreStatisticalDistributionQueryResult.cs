namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Result;
public class GetGenreStatisticalDistributionQueryResult
{
    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public int TotalSongCount { get; set; }
    public string AverageDurationFormatted { get; set; } 
    public string PopularityLevel { get; set; }
}

