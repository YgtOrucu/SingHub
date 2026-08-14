using SingHub.Application.Features.Auths.Result;

namespace SingHub.Application.Contract.Persistence;
public interface IJwtService
{
    Task<GetLoginQueryResult> GenerateTokenAsync(string UserName);
}
