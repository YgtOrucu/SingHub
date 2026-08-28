using SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Result;

namespace SingHub.Application.Contract.Persistence;
public interface IDashboardService
{
    Task<GetStatGridCardQueryResult> GetStatGridCardQueryAsync();
    Task<List<Top5MostListenedToSongsQueryResult>> GetMostListenedToSongsQueryResultsAsync();
    Task<List<GetGenreStatisticalDistributionQueryResult>> DistributionQueryResultsAsync();
    Task<List<GetRoleBasedUserDistributionQueryResult>> GetRoleBasedUserDistributionQueriesAsync();
    Task<GetIdentityVerificationStatusQueryResult> IdentityVerificationStatusQueryResultsAsync();
}
