using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Users.Commands;
using SingHub.Application.Features.ForAdminFeatures.Users.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Users.Endpoint;

public static class UsersEndpoint
{
    public record UpdateUserRoleDto(string RoleName);
    public static void RegisterUsersEndpoint(this IEndpointRouteBuilder builder)
    {
        var users = builder.MapGroup("/users").WithTags("Users");

        users.MapGet("GetAllUsers", GetAllUsersAsync);
        users.MapPut("UpdateUsersRoleName/{id}", UpdateUsersRoleAsync);
        users.MapGet("GetAllRoles", GetAllRolesAsync);
    }

    private static async Task<IResult> GetAllRolesAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetAllRoleNameQuery());
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> UpdateUsersRoleAsync(IMediator mediator, string Id, UpdateUserRoleDto dto)
    {
        var command = new UpdateUsersRoleCommand(Id, dto.RoleName);
        var response = await mediator.Send(command);
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetAllUsersAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetAllUsersQuery());
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }
}
