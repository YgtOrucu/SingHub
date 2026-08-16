using AutoMapper;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Mapping;

public class GenreMappingProfile : Profile
{
    public GenreMappingProfile()
    {
        CreateMap<CreateGenreCommand, Genre>();
        CreateMap<UpdateGenreCommand, Genre>();
    }
}
