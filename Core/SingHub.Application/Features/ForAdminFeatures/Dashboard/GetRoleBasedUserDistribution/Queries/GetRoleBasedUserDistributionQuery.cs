using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Queries;

public class GetRoleBasedUserDistributionQuery : IRequest<BaseResult<List<GetRoleBasedUserDistributionQueryResult>>>
{
}
