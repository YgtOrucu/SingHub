using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Result;


namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Handler.ReadProcess
{
    public class GetGenreStatisticalDistributionQueryHandler(IDashboardService service)
         : IRequestHandler<GetGenreStatisticalDistributionQuery, BaseResult<List<GetGenreStatisticalDistributionQueryResult>>>
    {

        public async Task<BaseResult<List<GetGenreStatisticalDistributionQueryResult>>> Handle(
            GetGenreStatisticalDistributionQuery request,
            CancellationToken cancellationToken)
        {
            var value = await service.DistributionQueryResultsAsync();

            return BaseResult<List<GetGenreStatisticalDistributionQueryResult>>.Success(value);
        }
    }
}
