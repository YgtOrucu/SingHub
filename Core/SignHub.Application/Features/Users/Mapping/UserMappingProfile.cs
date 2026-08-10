using AutoMapper;
using SignHub.Application.Features.Users.Commands;
using SignHub.Domain.Entities;

namespace SignHub.Application.Features.Users.Mapping;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AppUser, CreateUserCommand>().ReverseMap();
    }
}
