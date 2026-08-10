using MediatR;
using Microsoft.AspNetCore.Identity;
using SignHub.Application.Bases;
using SignHub.Application.Contract.Persistence;
using SignHub.Application.Exceptions;
using SignHub.Application.Features.Users.Queries;
using SignHub.Application.Features.Users.Result;
using SignHub.Domain.Entities;

namespace SignHub.Application.Features.Users.Handlers.ReadOperation;

public class GetLoginQueryHandle(UserManager<AppUser> userManager, IJwtService jwtService)
    : IRequestHandler<GetLoginQuery, BaseResult<GetLoginQueryResult>>
{
    public async Task<BaseResult<GetLoginQueryResult>> Handle(GetLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new NotFoundException("User not found");

        var result = await userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
            return BaseResult<GetLoginQueryResult>.Failure("Email or Password is incorrect.Please check details");

        var response = await jwtService.GenerateTokenAsync(user.UserName!);

        return BaseResult<GetLoginQueryResult>.Success(response);
    }
}
