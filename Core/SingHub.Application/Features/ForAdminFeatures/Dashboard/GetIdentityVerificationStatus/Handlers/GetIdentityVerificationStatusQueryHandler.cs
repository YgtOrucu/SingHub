using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Handlers;

public class GetIdentityVerificationStatusQueryHandler(IDashboardService service)
    : IRequestHandler<GetIdentityVerificationStatusQuery, BaseResult<GetIdentityVerificationStatusQueryResult>>
{
    public async Task<BaseResult<GetIdentityVerificationStatusQueryResult>> Handle(GetIdentityVerificationStatusQuery request, CancellationToken cancellationToken)
    {
        var values = await service.IdentityVerificationStatusQueryResultsAsync();
        return BaseResult<GetIdentityVerificationStatusQueryResult>.Success(values);
    }
}
