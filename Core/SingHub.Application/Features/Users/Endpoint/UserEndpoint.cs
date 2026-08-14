using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.Users.Commands;
using SingHub.Application.Features.Users.Queries;
using System.Security.Claims;

namespace SingHub.Application.Features.Users.Endpoint
{
    public static class UserEndpoint
    {
        public static void RegisterUsersEndpoint(this IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("/users").WithTags("Users");

            users.MapPost("register", CreateUserAsync);
            users.MapPost("login", LoginUserAsync);
            users.MapPost("forgotpassword", ForgotPasswordAsync);
            users.MapPost("resetpassword", ResetPasswordAsync);
            users.MapPost("logout", LogoutAsync);
        }

        private static async Task<IResult> LogoutAsync(IMediator mediator, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            var result = await mediator.Send(new UserLogoutCommand { UserId = userId });

            return Results.Ok(new { Message = result });
        }

        private static async Task<IResult> ResetPasswordAsync(IMediator mediator ,ResetPasswordCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        }

        private static async Task<IResult> ForgotPasswordAsync(IMediator mediator , ForgotPasswordCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
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
