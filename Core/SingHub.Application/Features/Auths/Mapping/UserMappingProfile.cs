using AutoMapper;
using SingHub.Application.Features.Auths.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.Auths.Mapping;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<AppUser, CreateUserCommand>().ReverseMap();
    }
}
