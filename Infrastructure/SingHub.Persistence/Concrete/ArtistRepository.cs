using Microsoft.EntityFrameworkCore;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Artists.Result;
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
            ArtistByAlbumCount = x.Albums.Where(x=>!x.IsDeleted).Count(),
            ArtistBySongCount = x.Songs.Where(x => !x.IsDeleted).Count()
        }).ToListAsync();
    }
}
