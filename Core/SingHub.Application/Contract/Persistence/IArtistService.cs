using SingHub.Application.Features.ForAdminFeatures.Artists.Result;

namespace SingHub.Application.Contract.Persistence
{
    public interface IArtistService
    {
        Task<List<GetArtistQueryResult>> GetArtistWithSongAndAlbumCount();
    }
}
