using AutoMapper;
using SingHub.Application.Features.ForAdminFeatures.Albums.Commands;
using SingHub.Application.Features.ForAdminFeatures.Albums.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Mapping
{
    internal class AlbumMappingProfile : Profile
    {
        public AlbumMappingProfile()
        {
            CreateMap<CreateAlbumCommand, Album>();
            CreateMap<UpdateAlbumCommand, Album>();

            CreateMap<Album, GetAlbumQueryResult>()
                .ForMember(desc => desc.SongCountByAlbum, opt => opt.MapFrom(src => src.Songs.Count))
                .ForMember(desc => desc.ArtistName, opt => opt.MapFrom(src => src.Artist.Name));

            CreateMap<Album, GetAlbumByIdQueryResult>()
                .ForMember(desc => desc.ArtistName, opt => opt.MapFrom(src => src.Artist.Name));
        }
    }
}
