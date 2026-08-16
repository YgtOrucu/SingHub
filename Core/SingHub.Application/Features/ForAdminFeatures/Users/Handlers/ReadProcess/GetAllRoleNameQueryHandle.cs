using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Users.Queries;
using SingHub.Application.Features.ForAdminFeatures.Users.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Users.Handlers.ReadProcess;

public class GetAllRoleNameQueryHandle(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
    : IRequestHandler<GetAllRoleNameQuery, BaseResult<List<GetAllRoleNameQueryResult>>>
{
    public async Task<BaseResult<List<GetAllRoleNameQueryResult>>> Handle(GetAllRoleNameQuery request, CancellationToken cancellationToken)
    {
        var getAllRoleName = roleManager.Roles.Select(x => new GetAllRoleNameQueryResult(x.Name!)).ToList();

        return BaseResult<List<GetAllRoleNameQueryResult>>.Success(getAllRoleName);
    }
}
