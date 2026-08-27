using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Handlers.ReadProcess;

public class GetStatGridCardQueryHandle(IDashboardService dashboardService) : IRequestHandler<GetStatGridCardQuery, BaseResult<GetStatGridCardQueryResult>>
{
    public async Task<BaseResult<GetStatGridCardQueryResult>> Handle(GetStatGridCardQuery request, CancellationToken cancellationToken)
    {
        var values = await dashboardService.GetStatGridCardQueryAsync();
        return BaseResult<GetStatGridCardQueryResult>.Success(values);
    }
}
