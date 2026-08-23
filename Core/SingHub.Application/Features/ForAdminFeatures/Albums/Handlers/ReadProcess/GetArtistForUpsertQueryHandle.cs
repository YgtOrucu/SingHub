using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Albums.Queries;
using SingHub.Application.Features.ForAdminFeatures.Albums.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Handlers.ReadProcess;

public class GetArtistForUpsertQueryHandle(IAlbumService repository)
    : IRequestHandler<GetArtistForUpsertQuery, BaseResult<List<GetArtistForUpsertQueryResult>>>
{
    public async Task<BaseResult<List<GetArtistForUpsertQueryResult>>> Handle(GetArtistForUpsertQuery request, CancellationToken cancellationToken)
    {
        var values = await repository.GetArtistForUpsertAsync();
        return BaseResult<List<GetArtistForUpsertQueryResult>>.Success(values);
    }
}
