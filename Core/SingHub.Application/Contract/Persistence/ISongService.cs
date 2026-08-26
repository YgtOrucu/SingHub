using SingHub.Application.Features.ForAdminFeatures.Songs.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Contract.Persistence;

public interface ISongService
{
    Task<List<GetSongQueryResult>> GetSongsWithDetailsAsync();
    Task<GetSongByIdQueryResult> GetSongWithDetailsByIdAsync(int id);

    Task<List<GetArtistForUpsertQueryResult>> GetArtistForUpsertAsync();
    Task<List<GetGenreForUpsertQueryResult>> GetGenreForUpsertAsync();
    Task<List<GetAlbumForUpsertQueryResult>> GetAlbumForUpsertAsync();
    Task<List<GetRoleForUpsertQueryResult>> GetRoleForUpsertAsync();
    Task<Song?> GetSongWithRolesByIdAsync(int id);
}