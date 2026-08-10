using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.Users.Result;

namespace SingHub.Application.Features.Users.Queries;

public record GetLoginQuery(string Email, string Password) : IRequest<BaseResult<GetLoginQueryResult>>;
