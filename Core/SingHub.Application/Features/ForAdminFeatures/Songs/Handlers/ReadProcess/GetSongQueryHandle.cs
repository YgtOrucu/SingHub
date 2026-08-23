using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Songs.Queries;
using SingHub.Application.Features.ForAdminFeatures.Songs.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Handlers.ReadProcess;

public class GetSongQueryHandler(ISongService repository)
    : IRequestHandler<GetSongQuery, BaseResult<List<GetSongQueryResult>>>
{
    public async Task<BaseResult<List<GetSongQueryResult>>> Handle(GetSongQuery request, CancellationToken cancellationToken)
    {
        var values = await repository.GetSongsWithDetailsAsync();
        return BaseResult<List<GetSongQueryResult>>.Success(values);
    }
}