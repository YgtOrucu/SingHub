using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SignHub.Application.Bases;
using SignHub.Application.Features.Users.Commands;
using SignHub.Domain.Entities;

namespace SignHub.Application.Features.Users.Handlers.WriteOperation;

public class CreateUserCommandHandle(UserManager<AppUser> userManager, IMapper mapper)
    : IRequestHandler<CreateUserCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<AppUser>(request);
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BaseResult<object>.Failure(result.Errors);

        return BaseResult<object>.Success(result);
    }
}
