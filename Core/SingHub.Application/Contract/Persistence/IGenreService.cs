using SingHub.Application.Features.ForAdminFeatures.Genres.Result;

namespace SingHub.Application.Contract.Persistence
{
    public interface IGenreService
    {
        Task<List<GetGenreQueryResult>> GetGenreWithSongCount();
    }
}
