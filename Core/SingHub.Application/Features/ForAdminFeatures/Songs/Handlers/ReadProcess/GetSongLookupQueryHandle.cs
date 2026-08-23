using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Songs.Queries;
using SingHub.Application.Features.ForAdminFeatures.Songs.Result;
namespace SingHub.Application.Features.ForAdminFeatures.Songs.Handlers.ReadProcess;

public class GetSongLookupQueryHandler(ISongService repository) :
    IRequestHandler<GetArtistForUpsertQuery, BaseResult<List<GetArtistForUpsertQueryResult>>>,
    IRequestHandler<GetGenreForUpsertQuery, BaseResult<List<GetGenreForUpsertQueryResult>>>,
    IRequestHandler<GetAlbumForUpsertQuery, BaseResult<List<GetAlbumForUpsertQueryResult>>>
{
    public async Task<BaseResult<List<GetArtistForUpsertQueryResult>>> Handle(GetArtistForUpsertQuery request, CancellationToken cancellationToken)
    {
        var data = await repository.GetArtistForUpsertAsync();
        return BaseResult<List<GetArtistForUpsertQueryResult>>.Success(data);
    }

    public async Task<BaseResult<List<GetGenreForUpsertQueryResult>>> Handle(GetGenreForUpsertQuery request, CancellationToken cancellationToken)
    {
        var data = await repository.GetGenreForUpsertAsync();
        return BaseResult<List<GetGenreForUpsertQueryResult>>.Success(data);
    }

    public async Task<BaseResult<List<GetAlbumForUpsertQueryResult>>> Handle(GetAlbumForUpsertQuery request, CancellationToken cancellationToken)
    {
        var data = await repository.GetAlbumForUpsertAsync();
        return BaseResult<List<GetAlbumForUpsertQueryResult>>.Success(data);
    }
}