using Microsoft.EntityFrameworkCore;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Result;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Result;
using SingHub.Persistence.Context;

namespace SingHub.Persistence.Concrete;

public class DashboardRepository(SingHubContext context) : IDashboardService
{
    public async Task<GetStatGridCardQueryResult> GetStatGridCardQueryAsync()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

        // 1. En Çok Dinlenen Tür
        var mostListenedGenre = await context.Songs
            .Where(x => !x.IsDeleted && x.Genre != null)
            .GroupBy(x => x.Genre.Name)
            .Select(g => new
            {
                GenreName = g.Key,
                TotalListenCount = g.Sum(s => s.ListenCount)
            })
            .OrderByDescending(x => x.TotalListenCount)
            .Select(x => x.GenreName)
            .FirstOrDefaultAsync();

        // 2. En Popüler Sanatçı
        var mostPopularArtist = await context.Songs
            .Where(x => !x.IsDeleted && x.Artist != null)
            .GroupBy(x => x.Artist.Name)
            .Select(g => new
            {
                ArtistName = g.Key,
                TotalListenCount = g.Sum(x => x.ListenCount)
            })
            .OrderByDescending(x => x.TotalListenCount)
            .FirstOrDefaultAsync();

        // 3. Toplam Dinlenme Sayısı
        var totalListenCount = await context.Songs
            .Where(x => !x.IsDeleted)
            .SumAsync(s => (long?)s.ListenCount) ?? 0;

        // 4. En Çok Dinlenen Şarkı
        var mostListenedSong = await context.Songs
            .Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.ListenCount)
            .Select(y => new { y.Title, y.ListenCount })
            .FirstOrDefaultAsync();

        // 5. Premium Kullanıcı Yüzdesi (DivideByZero Safe)
        var totalUsers = await context.Users.CountAsync();
        double premiumPercentage = 0;

        if (totalUsers > 0)
        {
            var premiumUsersCount = await context.UserRoles
                .Join(
                    context.Roles,
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { userRole, role }
                )
                .Where(x => x.role.Name == "Premium")
                .CountAsync();

            premiumPercentage = Math.Round(((double)premiumUsersCount / totalUsers) * 100, 1);
        }

        // 6. Ortalama Şarkı Süresi
        var avgSeconds = await context.Songs
            .Where(x => !x.IsDeleted)
            .Select(x => (double?)x.Duration)
            .AverageAsync() ?? 0;

        TimeSpan durationSpan = TimeSpan.FromSeconds(avgSeconds);

        // 7. Albüm Başına Düşen Şarkı Ortalama (DivideByZero Safe)
        var totalAlbums = await context.Albums.Where(a => !a.IsDeleted).CountAsync();
        double roundedAlbumAvg = 0;

        if (totalAlbums > 0)
        {
            var totalSongsInAlbums = await context.Songs
                .Where(s => !s.IsDeleted && s.AlbumId != null)
                .CountAsync();

            roundedAlbumAvg = Math.Round((double)totalSongsInAlbums / totalAlbums, 1);
        }

        // 8. Aktif Yayınlanan Ülke Sayısı
        var activeCountryCount = await context.Artists
            .Where(a => !a.IsDeleted && !string.IsNullOrWhiteSpace(a.Country))
            .Select(a => a.Country)
            .Distinct()
            .CountAsync();

        // 9. Silinmiş / Pasif İçerik Sayısı (Query Filter baypas edilerek)
        var deletedSongs = await context.Songs.IgnoreQueryFilters().CountAsync(s => s.IsDeleted);
        var deletedAlbums = await context.Albums.IgnoreQueryFilters().CountAsync(a => a.IsDeleted);
        var deletedArtists = await context.Artists.IgnoreQueryFilters().CountAsync(a => a.IsDeleted);
        var archivedItemCount = deletedSongs + deletedAlbums + deletedArtists;

        // 10. İki Faktörlü Doğrulama Kullanan Kullanıcı Sayısı
        var twoFactorUserCount = await context.Users.CountAsync(u => u.TwoFactorEnabled);

        // 11. Bu Ay Eklenen Yeni Şarkı Sayısı
        var monthlyAddedSongCount = await context.Songs
            .Where(s => !s.IsDeleted && s.CreatedDate >= startOfMonth)
            .CountAsync();

        // 12. Kilitli / Engelli Hesap Sayısı
        var lockedOutUserCount = await context.Users
            .CountAsync(u => u.LockoutEnd.HasValue && u.LockoutEnd.Value > now);

        return new GetStatGridCardQueryResult
        {
            MostListenedGenre = mostListenedGenre ?? "N/A",
            MostPopularArtistName = mostPopularArtist?.ArtistName ?? "N/A",
            MostPopularArtistListenCount = FormatListenCount(mostPopularArtist?.TotalListenCount ?? 0),
            TotalListenCount = totalListenCount,
            MostListenedSongTitle = mostListenedSong?.Title ?? "N/A",
            MostListenedSongListenCount = FormatListenCount(mostListenedSong?.ListenCount ?? 0),
            PremiumUserPercentage = premiumPercentage,
            AverageSongDurationFormatted = $"{durationSpan.Minutes} dk {durationSpan.Seconds} s",
            AverageSongsPerAlbumFormatted = $"{roundedAlbumAvg:0.0} Parça",
            ActiveCountryCount = activeCountryCount,
            ArchivedItemCount = archivedItemCount,
            TwoFactorEnabledUserCount = twoFactorUserCount,
            MonthlyAddedSongCount = monthlyAddedSongCount,
            LockedOutUserCount = lockedOutUserCount
        };
    }

    private static string FormatListenCount(long count)
    {
        if (count >= 1_000_000)
            return $"{count / 1_000_000.0:0.#} M";

        if (count >= 1_000)
            return $"{count / 1_000.0:0.#} K";

        return count.ToString();
    }

    public async Task<List<Top5MostListenedToSongsQueryResult>> GetMostListenedToSongsQueryResultsAsync()
    {
        return await context.Songs
           .AsNoTracking()
           .Where(x => !x.IsDeleted)
           .Select(x => new Top5MostListenedToSongsQueryResult
           {
               Title = x.Title,
               ListenCount = x.ListenCount,
               ArtistName = x.Artist.Name,
               GenreName = x.Genre.Name,
           })
           .OrderByDescending(x => x.ListenCount).Take(5).ToListAsync();
    }

    public async Task<List<GetGenreStatisticalDistributionQueryResult>> DistributionQueryResultsAsync()
    {
        return await context.Genres
            .Select(g => new GetGenreStatisticalDistributionQueryResult
            {
                GenreId = g.Id,
                GenreName = g.Name,
                TotalSongCount = g.Songs.Count,
                AverageDurationFormatted = g.Songs.Any()
                        ? TimeSpan.FromSeconds(g.Songs.Average(s => s.Duration)).ToString(@"m\:ss") + " min"
                        : "0:00 min",
                PopularityLevel = g.Songs.Sum(s => s.ListenCount) > 50000 ? "Yüksek" : "Orta"
            })
                .ToListAsync();
    }

    public async Task<List<GetRoleBasedUserDistributionQueryResult>> GetRoleBasedUserDistributionQueriesAsync()
    {
        return await (from role in context.Roles
                      join userRole in context.UserRoles on role.Id equals userRole.RoleId into userRolesGroup
                      select new GetRoleBasedUserDistributionQueryResult
                      {
                          RoleName = role.Name,
                          UserCount = userRolesGroup.Count(),
                          AccessLevel = role.Name == "Admin" ? "Tam Yetki" :
                                       role.Name == "Artist" ? "İçerik Yönetimi" :
                                       role.Name == "Premium" ? "Dinleyici (Sınırsız)" : "Dinleyici (Kısıtlı)",
                          Status = role.Name == "Free" ? "Kısıtlı" : "Aktif"
                      }).ToListAsync();
    }

    public async Task<GetIdentityVerificationStatusQueryResult> IdentityVerificationStatusQueryResultsAsync()
    {
        var totalUsers = await context.Users.CountAsync();
        var confirmedEmailCount = await context.Users.CountAsync(u => u.EmailConfirmed);
        var twoFactorCount = await context.Users.CountAsync(u => u.TwoFactorEnabled);
        var lockedOutCount = await context.Users.CountAsync(u => u.LockoutEnd.HasValue && u.LockoutEnd > DateTimeOffset.UtcNow);

        double percentage = totalUsers > 0 ? (double)confirmedEmailCount / totalUsers * 100 : 0;

        return new GetIdentityVerificationStatusQueryResult
        {
            ConfirmedEmailCount = confirmedEmailCount,
            ConfirmedEmailPercentage = Math.Round(percentage, 1),
            TwoFactorEnabledCount = twoFactorCount,
            LockedOutUserCount = lockedOutCount
        };
    }
}