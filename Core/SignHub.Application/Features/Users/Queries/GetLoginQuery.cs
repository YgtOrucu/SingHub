using MediatR;
using SignHub.Application.Bases;
using SignHub.Application.Features.Users.Result;

namespace SignHub.Application.Features.Users.Queries;

public record GetLoginQuery(string Email, string Password) : IRequest<BaseResult<GetLoginQueryResult>>;
