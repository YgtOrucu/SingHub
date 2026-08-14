using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.Auths.Commands;
public class ForgotPasswordCommand :IRequest<BaseResult<string>>
{
    public string Email { get; set; }
}
