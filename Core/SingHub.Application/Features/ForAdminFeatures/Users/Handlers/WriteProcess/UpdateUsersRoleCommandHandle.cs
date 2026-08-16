using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Bases;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForAdminFeatures.Users.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Users.Handlers.WriteProcess;

public class UpdateUsersRoleCommandHandle(UserManager<AppUser> userManager)
    : IRequestHandler<UpdateUsersRoleCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UpdateUsersRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null)
            throw new NotFoundException("User not found.");

        var currentRoles = await userManager.GetRolesAsync(user);

        if (currentRoles.Contains(request.RoleName))
        {
            return BaseResult<object>.Success("User already has this role.");
        }

        if (currentRoles.Any())
        {
            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
                throw new Exception("An error occurred while removing existing roles.");
        }

        var addResult = await userManager.AddToRoleAsync(user, request.RoleName);
        if (!addResult.Succeeded)
            throw new Exception("An error occurred while adding the new role.");

        return BaseResult<object>.Success("User role updated successfully.");
    }
}
