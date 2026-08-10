using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.Users.Commands;
using SingHub.Application.Features.Users.Queries;

namespace SingHub.Application.Features.Users.Endpoint
{
    public static class UserEndpoint
    {
        public static void RegisterUsersEndpoint(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/users").WithTags("Users");

            users.MapPost("register", CreateUserAsync);
            users.MapPost("login", LoginUserAsync);
        }

        private static async Task<IResult> CreateUserAsync(IMediator mediator, CreateUserCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }

        private static async Task<IResult> LoginUserAsync(IMediator mediator, GetLoginQuery getLogin)
        {
            var result = await mediator.Send(getLogin);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }
    }
}
