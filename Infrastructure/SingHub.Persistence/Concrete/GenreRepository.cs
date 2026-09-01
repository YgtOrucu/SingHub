using Microsoft.EntityFrameworkCore;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Genres.Result;
using SingHub.Persistence.Context;

namespace SingHub.Persistence.Concrete;

public class GenreRepository(SingHubContext context) : IGenreService
{
    public async Task<List<GetGenreQueryResult>> GetGenreWithSongCount()
    {
        return await context.Genres.AsNoTracking().Select(x => new GetGenreQueryResult
        {
            Id = x.Id,
            ImageUrl = x.ImageUrl,
            Name = x.Name,
            Description = x.Description,
            SongByGenreCount = x.Songs.Where(x => !x.IsDeleted).Count(),
            IsDeleted = x.IsDeleted

        }).ToListAsync();
    }
}
