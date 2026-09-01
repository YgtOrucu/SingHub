using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForUsersFeatures.PermissionToSongPlay.Queries;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForUsersFeatures.PermissionToSongPlay.Handlers;

public class CheckPlayAccessQueryHandle(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper, IGenericRepository<Song> repository, IGenericRepository<SongAppRole> _songAppRoleservice)
    : IRequestHandler<CheckPlayAccessQuery, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CheckPlayAccessQuery request, CancellationToken cancellationToken)
    {
        var songvalue = await repository.GetByIdAsync(request.SongId);
        if (songvalue == null)
        {
            throw new NotFoundException("Song could not found");
        }

        var getUserDetails = await userManager.FindByNameAsync(request.UserName);
        if (getUserDetails == null)
        {
            throw new NotFoundException("User could not found");
        }

        var getUserRole = await userManager.GetRolesAsync(getUserDetails);
        if(getUserRole.Contains("Admin"))
        {
            return BaseResult<object>.Success("Access successful.", true);
        }

        var songRoleIds = _songAppRoleservice.GetByQuery().Where(x => x.SongId == request.SongId).Select(y => y.RoleId).ToList();

        if (!songRoleIds.Any())
        {
            return BaseResult<object>.Success("Access successful.", true);
        }

        var songRoleNames = new List<string>();
        foreach (var roleId in songRoleIds)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role?.Name != null)
            {
                songRoleNames.Add(role.Name);
            }
        }
        bool hasAccess = getUserRole.Intersect(songRoleNames).Any();

        if (!hasAccess)
        {
            throw new UnAuthorizationException("You do not have the required role to listen to this song.");
        }

        return BaseResult<object>.Success("Access successful.", true);
    }
}
