using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Albums.Queries;
using SingHub.Application.Features.ForAdminFeatures.Albums.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Handlers.ReadProcess;

public class GetAlbumByIdQueryHandle(IAlbumService repository)
    : IRequestHandler<GetAlbumByIdQuery, BaseResult<GetAlbumByIdQueryResult>>
{
    public async Task<BaseResult<GetAlbumByIdQueryResult>> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        var value = await repository.GetAlbumListWithArtistAsync(request.Id);
        return BaseResult<GetAlbumByIdQueryResult>.Success(value);
    }
}
