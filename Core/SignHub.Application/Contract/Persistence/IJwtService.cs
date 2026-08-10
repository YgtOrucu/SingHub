using SignHub.Application.Features.Users.Result;

namespace SignHub.Application.Contract.Persistence;
public interface IJwtService
{
    Task<GetLoginQueryResult> GenerateTokenAsync(string UserName);
}
