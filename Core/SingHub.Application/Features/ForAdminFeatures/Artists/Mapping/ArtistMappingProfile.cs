using AutoMapper;
using SingHub.Application.Features.ForAdminFeatures.Artists.Commands;
using SingHub.Application.Features.ForAdminFeatures.Artists.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Mapping;

public class ArtistMappingProfile : Profile
{
    public ArtistMappingProfile()
    {
        CreateMap<CreateArtistCommand, Artist>();
        CreateMap<UpdateArtistCommand, Artist>();
        CreateMap<Artist, GetArtistByIdQueryResult>();
    }
}
