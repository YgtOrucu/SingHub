using AutoMapper;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;
using SingHub.Application.Features.ForAdminFeatures.Genres.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Mapping;

public class GenreMappingProfile : Profile
{
    public GenreMappingProfile()
    {
        CreateMap<CreateGenreCommand, Genre>();
        CreateMap<UpdateGenreCommand, Genre>();

        CreateMap<Genre, GetGenreQueryResult>().ForMember(desc => desc.SongByGenreCount, opt => opt.MapFrom(src => src.Songs.Count));
        CreateMap<Genre, GetGenreByIdQueryResult>();
    }
}
