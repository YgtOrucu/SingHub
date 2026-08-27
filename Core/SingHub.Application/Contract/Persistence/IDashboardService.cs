using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Result;

namespace SingHub.Application.Contract.Persistence;
public interface IDashboardService
{
    Task<GetStatGridCardQueryResult> GetStatGridCardQueryAsync();
}
