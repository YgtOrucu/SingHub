using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Queries;

public class Top5MostListenedToSongsQuery : IRequest<BaseResult<List<Top5MostListenedToSongsQueryResult>>>
{
}
