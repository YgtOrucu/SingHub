using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Users.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Users.Queries;

public record GetAllRoleNameQuery : IRequest<BaseResult<List<GetAllRoleNameQueryResult>>>;
