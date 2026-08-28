namespace SingHub.Dto.ForAdminPageDtos.DashboardDto.SpeciesBasedStatisticalDistributionDto;
public class ResultSpeciesBasedStatisticalDistribution
{
    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public int TotalSongCount { get; set; }
    public string AverageDurationFormatted { get; set; }
    public string PopularityLevel { get; set; }
}
