using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.Auths.Result;

namespace SingHub.Application.Features.Auths.Queries;

public record GetLoginQuery(string Email, string Password) : IRequest<BaseResult<GetLoginQueryResult>>;
