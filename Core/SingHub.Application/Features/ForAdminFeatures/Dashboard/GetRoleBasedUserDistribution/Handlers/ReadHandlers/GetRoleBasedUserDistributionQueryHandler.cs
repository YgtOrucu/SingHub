using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Handlers.ReadHandlers;

public class GetRoleBasedUserDistributionQueryHandler(IDashboardService service)
        : IRequestHandler<GetRoleBasedUserDistributionQuery, BaseResult<List<GetRoleBasedUserDistributionQueryResult>>>
{

    public async Task<BaseResult<List<GetRoleBasedUserDistributionQueryResult>>> Handle(
        GetRoleBasedUserDistributionQuery request,
        CancellationToken cancellationToken)
    {
        var value = await service.GetRoleBasedUserDistributionQueriesAsync();

        return BaseResult<List<GetRoleBasedUserDistributionQueryResult>>.Success(value);
    }
}
