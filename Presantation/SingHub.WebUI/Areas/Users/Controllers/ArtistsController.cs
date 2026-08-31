using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForPresantationPageDtos.ArtistsDto;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Areas.Users.Controllers
{
    [Area("Users")]
    public class ArtistsController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient("SingHubAPI");

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("artist");

            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<BaseResult<List<ResultArtistsDto>>>();
                return View(value!.Data);
            }
            var errorResult = await response.Content.ReadFromJsonAsync<BaseResult<ApiResponseError>>();
            TempData["ErrorMessage"] = errorResult?.Message ?? "You do not have permission to access";
            return View(new List<ResultArtistsDto>());
        }

        [HttpGet]
        public async Task<IActionResult> ArtistSongs(int id)
        {
            var response = await _client.GetAsync($"artistsDetails?Id={id}");
            if (response.IsSuccessStatusCode)
            {
                var values = await response.Content.ReadFromJsonAsync<BaseResult<ResultArtistWithSongsDto>>();
                return View(values!.Data);
            }
            return View(new ResultArtistWithSongsDto());
        }
    }
}
