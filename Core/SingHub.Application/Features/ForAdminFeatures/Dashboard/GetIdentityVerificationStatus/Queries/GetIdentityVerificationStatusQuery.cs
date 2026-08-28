using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Queries;

public class GetIdentityVerificationStatusQuery : IRequest<BaseResult<GetIdentityVerificationStatusQueryResult>>
{
}
