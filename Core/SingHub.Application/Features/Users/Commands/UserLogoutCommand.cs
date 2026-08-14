using MediatR;

namespace SingHub.Application.Features.Users.Commands;

public class UserLogoutCommand : IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
}
