namespace SingHub.Application.Features.Users.Result;
public class GetLoginQueryResult
{
    public string Token { get; set; }
    public DateTime ExpirationTime { get; set; }
}
