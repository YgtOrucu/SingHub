using AutoMapper;
using SingHub.Application.Features.ForAdminFeatures.Songs.Commands;
using SingHub.Application.Features.ForAdminFeatures.Songs.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Mapping;

internal class SongMappingProfile : Profile
{
    public SongMappingProfile()
    {
        CreateMap<CreateSongCommand, Song>();
        CreateMap<UpdateSongCommand, Song>();

        CreateMap<Song, GetSongQueryResult>()
            .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album != null ? src.Album.Title : null));

        CreateMap<Song, GetSongByIdQueryResult>()
            .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.AlbumTitle, opt => opt.MapFrom(src => src.Album != null ? src.Album.Title : null));
    }
}