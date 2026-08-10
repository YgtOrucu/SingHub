using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.Users.Queries;
using SingHub.Application.Features.Users.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.Users.Handlers.ReadOperation;

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
