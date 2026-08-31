using SingHub.Application.Features.ForAdminFeatures.Artists.Result;
using SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Result;

namespace SingHub.Application.Contract.Persistence
{
    public interface IArtistService
    {
        Task<List<GetArtistQueryResult>> GetArtistWithSongAndAlbumCount();
        Task<ArtistDetailsAndSongQueryResult> GetArtistDetailsAndSongAsync(int id);
    }
}
