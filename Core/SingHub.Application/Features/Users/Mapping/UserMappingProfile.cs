using AutoMapper;
using SingHub.Application.Features.Users.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.Users.Mapping;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AppUser, CreateUserCommand>().ReverseMap();
    }
}
