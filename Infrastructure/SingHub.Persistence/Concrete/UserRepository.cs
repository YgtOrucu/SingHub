using Microsoft.EntityFrameworkCore;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Users.Result;
using SingHub.Persistence.Context;

namespace SingHub.Persistence.Concrete;

public class UserRepository(SingHubContext _context) : IUserRepository
{
    public async Task<List<GetAllUsersQueryResult>> GetAllUsersAsync()
    {
        return await _context.Users
            .Select(user => new GetAllUsersQueryResult
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty,

                RoleName = _context.UserRoles
                    .Where(ur => ur.UserId == user.Id)
                    .Join(
                        _context.Roles,
                        ur => ur.RoleId,
                        role => role.Id,
                        (ur, role) => role.Name
                    )
                    .FirstOrDefault() ?? string.Empty
            }).ToListAsync();
    }
}