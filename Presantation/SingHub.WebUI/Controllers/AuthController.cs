using Microsoft.AspNetCore.Mvc;
using SingHub.Dto.AuthDtos;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Controllers
{
    public class AuthController(IHttpClientFactory _httpClient) : Controller
    {
        private readonly HttpClient _client = _httpClient.CreateClient("SingHubAPI");

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await _client.PostAsJsonAsync("users/register", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            var result = await response.Content.ReadFromJsonAsync<ApiResponseError<object>>();
            if (result?.Errors != null)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Message ?? "An error occurred.");
                }
            }
            return View(dto);
        }

    }
}
