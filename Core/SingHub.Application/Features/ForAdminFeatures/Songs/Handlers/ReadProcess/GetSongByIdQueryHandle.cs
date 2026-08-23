using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Songs.Queries;
using SingHub.Application.Features.ForAdminFeatures.Songs.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Handlers.ReadProcess;

public class GetSongByIdQueryHandler(ISongService repository)
    : IRequestHandler<GetSongByIdQuery, BaseResult<GetSongByIdQueryResult>>
{
    public async Task<BaseResult<GetSongByIdQueryResult>> Handle(GetSongByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await repository.GetSongWithDetailsByIdAsync(request.Id);
        return BaseResult<GetSongByIdQueryResult>.Success(value);
    }
}