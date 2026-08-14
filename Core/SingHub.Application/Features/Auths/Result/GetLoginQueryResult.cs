namespace SingHub.Application.Features.Auths.Result;
public class GetLoginQueryResult
{
    public string Token { get; set; }
    public DateTime ExpirationTime { get; set; }
}
