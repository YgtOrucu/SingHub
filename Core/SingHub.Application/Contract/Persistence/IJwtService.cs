using SingHub.Application.Features.Users.Result;

namespace SingHub.Application.Contract.Persistence;
public interface IJwtService
{
    Task<GetLoginQueryResult> GenerateTokenAsync(string UserName);
}
