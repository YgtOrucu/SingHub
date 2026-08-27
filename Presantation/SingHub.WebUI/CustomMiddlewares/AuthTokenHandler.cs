using SingHub.Application.Bases;
using SingHub.WebUI.Models;
using System.Net;
using System.Net.Http.Headers;

namespace SingHub.WebUI.CustomMiddlewares;

public class AuthTokenHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = httpContextAccessor.HttpContext?.User.FindFirst("AccessToken")?.Value;

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response =  await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var customError = new BaseResult<ApiResponseError>
            {
                Message = "You do not have permission to access this page or resource. Please log in again.",
            };
            response.Content = JsonContent.Create(customError);
        }

        return response;
    }
}
