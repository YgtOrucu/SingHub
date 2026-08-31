using Microsoft.EntityFrameworkCore;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Artists.Result;
using SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Result;
using SingHub.Persistence.Context;

namespace SingHub.Persistence.Concrete;

public class ArtistRepository(SingHubContext context) : IArtistService
{

    public async Task<List<GetArtistQueryResult>> GetArtistWithSongAndAlbumCount()
    {
        return await context.Artists.AsNoTracking().Select(x => new GetArtistQueryResult
        {
            Id = x.Id,
            Biography = x.Biography,
            BannerUrl = x.BannerUrl,
            BirthDate = x.BirthDate,
            Country = x.Country,
            ImageUrl = x.ImageUrl,
            Name = x.Name,
            ArtistByAlbumCount = x.Albums.Where(x => !x.IsDeleted).Count(),
            ArtistBySongCount = x.Songs.Where(x => !x.IsDeleted).Count()
        }).ToListAsync();
    }

    public async Task<ArtistDetailsAndSongQueryResult> GetArtistDetailsAndSongAsync(int id)
    {
        return await context.Artists.Where(a => a.Id == id).Select(a => new ArtistDetailsAndSongQueryResult
         {
             ArtistDto = new ArtistDto
             {
                 Id = a.Id,
                 Name = a.Name,
                 Country = a.Country,
                 BannerUrl = a.BannerUrl,
                 Biography = a.Biography,
                 BirthDate = a.BirthDate
             },
             SongDtos = a.Songs.Select(s => new SongDto
             {
                 Id = s.Id,
                 Title = s.Title,
                 Duration = s.Duration,
                 AudioUrl = s.AudioUrl,
                 CoverImageUrl = s.CoverImageUrl
             }).ToList()
         })
         .FirstOrDefaultAsync();
    }
}
