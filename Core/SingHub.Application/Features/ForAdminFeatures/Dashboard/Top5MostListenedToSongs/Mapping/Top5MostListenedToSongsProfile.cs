using AutoMapper;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Mapping;

public class Top5MostListenedToSongsProfile : Profile
{
    public Top5MostListenedToSongsProfile()
    {
        CreateMap<Song, Top5MostListenedToSongsQueryResult>();
    }
}
