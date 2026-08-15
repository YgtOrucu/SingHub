using MediatR;

namespace SingHub.Application.Features.Auths.Commands;

public class LogoutCommand : IRequest<string>
{
    public string UserId { get; set; } = string.Empty;
}
