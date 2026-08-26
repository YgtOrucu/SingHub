using AutoMapper;
using SingHub.Application.Features.ForAdminFeatures.Albums.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Mapping
{
    public class AlbumMappingProfile : Profile
    {
        public AlbumMappingProfile()
        {
            CreateMap<CreateAlbumCommand, Album>();
            CreateMap<UpdateAlbumCommand, Album>();
        }
    }
}
