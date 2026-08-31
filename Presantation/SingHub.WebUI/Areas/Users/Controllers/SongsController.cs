using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForPresantationPageDtos.SongsDto;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    public class SongsController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient("SingHubAPI");

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("song");

            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<BaseResult<List<ResultSongsDto>>>();
                return View(value!.Data);
            }
            var errorResult = await response.Content.ReadFromJsonAsync<BaseResult<ApiResponseError>>();
            TempData["ErrorMessage"] = errorResult?.Message ?? "You do not have permission to access";
            return View(new List<ResultSongsDto>());
        }

        [HttpPost]
        public async Task<IActionResult> CheckPlayAccess(int songId)
        {
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized();
            }
            var requestUrl = $"permissionToSongPlay?SongId={songId}&UserName={Uri.EscapeDataString(userName)}";

            var response = await _client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<object>();
                return Ok(result);
            }
            var errorResult = await response.Content.ReadFromJsonAsync<BaseResult<ApiResponseError>>();
            var errorMessage = errorResult?.Message ?? "You do not have the required role to listen to this song.";

            TempData["ErrorMessage"] = errorMessage;

            return StatusCode((int)response.StatusCode, new
            {
                allowed = false,
                message = errorMessage
            });
        }
    }
}
