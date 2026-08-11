namespace SingHub.WebUI.Models
{
    public class GetJwtTokenInfo
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
