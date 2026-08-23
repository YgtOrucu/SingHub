using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Albums.Queries;
using SingHub.Application.Features.ForAdminFeatures.Albums.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Handlers.ReadProcess;

public class GetAlbumQueryHandle(IAlbumService repository)
    : IRequestHandler<GetAlbumQuery, BaseResult<List<GetAlbumQueryResult>>>
{
    public async Task<BaseResult<List<GetAlbumQueryResult>>> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
    {
        var values = await repository.GetAlbumListWithArtistAsync();
        return BaseResult<List<GetAlbumQueryResult>>.Success(values);
    }
}
