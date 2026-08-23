using Microsoft.EntityFrameworkCore;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Songs.Result;
using SingHub.Domain.Entities;
using SingHub.Persistence.Context;

namespace SingHub.Persistence.Concrete;

public class SongRepository(SingHubContext context) : ISongService
{
    public async Task<List<GetAlbumForUpsertQueryResult>> GetAlbumForUpsertAsync()
    {
        return await context.Albums.AsNoTracking().Select(x => new GetAlbumForUpsertQueryResult { Id = x.Id, Title = x.Title }).ToListAsync();
    }

    public async Task<List<GetArtistForUpsertQueryResult>> GetArtistForUpsertAsync()
    {
        return await context.Artists.AsNoTracking().Select(x => new GetArtistForUpsertQueryResult { Id = x.Id, Name = x.Name }).ToListAsync();
    }

    public async Task<List<GetGenreForUpsertQueryResult>> GetGenreForUpsertAsync()
    {
       return  await context.Genres.AsNoTracking().Select(x => new GetGenreForUpsertQueryResult { Id = x.Id, Name = x.Name }).ToListAsync();
    }

    public async Task<List<GetSongQueryResult>> GetSongsWithDetailsAsync()
    {
        return await context.Set<Song>()
            .AsNoTracking()
            .Select(x => new GetSongQueryResult
            {
                Id = x.Id,
                Title = x.Title,
                Duration = x.Duration,
                AudioUrl = x.AudioUrl,
                CoverImageUrl = x.CoverImageUrl,
                ListenCount = x.ListenCount,
                ReleaseDate = x.ReleaseDate,
                AlbumId = x.AlbumId,
                ArtistId = x.ArtistId,
                GenreId = x.GenreId,
                ArtistName = x.Artist.Name,
                GenreName = x.Genre.Name,
                AlbumTitle = x.Album != null ? x.Album.Title : null,
                IsDeleted = x.IsDeleted,
            })
            .ToListAsync();
    }

    public async Task<GetSongByIdQueryResult> GetSongWithDetailsByIdAsync(int id)
    {
        return await context.Set<Song>()
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new GetSongByIdQueryResult
            {
                Id = x.Id,
                Title = x.Title,
                Duration = x.Duration,
                AudioUrl = x.AudioUrl,
                CoverImageUrl = x.CoverImageUrl,
                ListenCount = x.ListenCount,
                ReleaseDate = x.ReleaseDate,
                AlbumId = x.AlbumId,
                ArtistId = x.ArtistId,
                GenreId = x.GenreId,
                ArtistName = x.Artist.Name,
                GenreName = x.Genre.Name,
                AlbumTitle = x.Album != null ? x.Album.Title : null
            })
            .FirstOrDefaultAsync();
    }
}