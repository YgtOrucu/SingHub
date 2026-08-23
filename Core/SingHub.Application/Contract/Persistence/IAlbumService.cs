using SingHub.Application.Features.ForAdminFeatures.Albums.Result;

namespace SingHub.Application.Contract.Persistence;
public interface IAlbumService
{
    Task<List<GetAlbumQueryResult>> GetAlbumListWithArtistAsync();
    Task<List<GetArtistForUpsertQueryResult>> GetArtistForUpsertAsync();
    Task<GetAlbumByIdQueryResult> GetAlbumListWithArtistAsync(int Id);
}
