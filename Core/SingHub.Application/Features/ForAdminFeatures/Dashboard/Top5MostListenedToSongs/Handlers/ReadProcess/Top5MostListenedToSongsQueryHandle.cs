using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Handlers.ReadProcess;

public class Top5MostListenedToSongsQueryHandle(IDashboardService service, IMapper mapper)
    : IRequestHandler<Top5MostListenedToSongsQuery, BaseResult<List<Top5MostListenedToSongsQueryResult>>>
{
    public async Task<BaseResult<List<Top5MostListenedToSongsQueryResult>>> Handle(Top5MostListenedToSongsQuery request, CancellationToken cancellationToken)
    {
        var values = await service.GetMostListenedToSongsQueryResultsAsync();

        return BaseResult<List<Top5MostListenedToSongsQueryResult>>.Success(values);
    }
}
