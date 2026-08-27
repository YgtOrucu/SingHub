using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Queries;

public class GetStatGridCardQuery : IRequest<BaseResult<GetStatGridCardQueryResult>>
{
}
