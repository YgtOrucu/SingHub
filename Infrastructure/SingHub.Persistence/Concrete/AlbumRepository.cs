using Microsoft.EntityFrameworkCore;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Albums.Result;
using SingHub.Persistence.Context;

namespace SingHub.Persistence.Concrete;

public class AlbumRepository(SingHubContext context) : IAlbumService
{
    public async Task<List<GetAlbumQueryResult>> GetAlbumListWithArtistAsync()
    {
        return await context.Albums.AsNoTracking().Select(x => new GetAlbumQueryResult
        {
            Id = x.Id,
            ArtistId = x.ArtistId,
            ArtistName = x.Artist.Name,
            CoverImageUrl = x.CoverImageUrl,
            ReleaseDate = x.ReleaseDate,
            Title = x.Title,
            SongCountByAlbum = x.Songs.Count,
            IsDeleted = x.IsDeleted
        }).ToListAsync();
    }

    public async Task<GetAlbumByIdQueryResult> GetAlbumListWithArtistAsync(int Id)
    {
        return await context.Albums.AsNoTracking().Where(x => x.Id == Id).Select(x => new GetAlbumByIdQueryResult
        {
            Id = x.Id,
            ArtistId = x.ArtistId,
            ArtistName = x.Artist.Name,
            CoverImageUrl = x.CoverImageUrl,
            ReleaseDate = x.ReleaseDate,
            Title = x.Title,
        }).FirstOrDefaultAsync();
    }

    public Task<List<GetArtistForUpsertQueryResult>> GetArtistForUpsertAsync()
    {
        return context.Artists.AsNoTracking().Select(x => new GetArtistForUpsertQueryResult
        {
            Id = x.Id,
            Name = x.Name,
        }).ToListAsync();
    }
}
