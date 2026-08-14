using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Users.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Users.Endpoint;

public static class UsersEndpoint
{
    public static void RegisterUsersEndpoint(this IEndpointRouteBuilder builder)
    {
        var users = builder.MapGroup("/users").WithTags("Users");

        users.MapGet("GetAllUsers", GetAllUsersAsync);
    }

    private static async Task<IResult> GetAllUsersAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetAllUsersQuery());
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }
}
