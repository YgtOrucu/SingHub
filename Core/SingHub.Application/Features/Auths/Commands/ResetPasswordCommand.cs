using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.Auths.Commands;

public class ResetPasswordCommand : IRequest<BaseResult<string>>
{
    public string Email { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}
