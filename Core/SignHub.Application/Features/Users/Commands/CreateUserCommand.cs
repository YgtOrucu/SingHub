using MediatR;
using SignHub.Application.Bases;

namespace SignHub.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<BaseResult<object>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
