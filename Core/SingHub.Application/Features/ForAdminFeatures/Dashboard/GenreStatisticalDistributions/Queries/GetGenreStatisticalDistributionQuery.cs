using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Queries;

public class GetGenreStatisticalDistributionQuery : IRequest<BaseResult<List<GetGenreStatisticalDistributionQueryResult>>>
{
}
