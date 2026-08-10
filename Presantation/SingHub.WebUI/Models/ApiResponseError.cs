namespace SingHub.WebUI.Models
{
    public class ApiResponseError<T>
    {
        public List<ApiErrorDetail>? Errors { get; set; }
    }

    public class ApiErrorDetail
    {
        public string Code { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
