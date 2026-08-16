using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Users.Commands;

public record UpdateUsersRoleCommand(string Id, string RoleName) : IRequest<BaseResult<object>>;