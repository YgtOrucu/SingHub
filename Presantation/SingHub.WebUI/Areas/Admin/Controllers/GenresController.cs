using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.GenresDto;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenresController(IHttpClientFactory httpClientFactory) : AdminBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.GetAsync("genre");
            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<BaseResult<List<ResultGenresDto>>>();
                return View(value!.Data);
            }
            var errorResult = await response.Content.ReadFromJsonAsync<BaseResult<ApiResponseError>>();
            ViewData["ErrorMessage"] = errorResult?.Message ?? "You do not have permission to access";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateGenres()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenres(CreateGenresDto dto)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.PostAsJsonAsync("genre", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await HandleApiErrorsAsync(response);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGenres(int id)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.GetAsync($"genre/{id}");
            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<BaseResult<UpdateGenresDto>>();
                return View(value!.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGenres(UpdateGenresDto dto)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.PutAsJsonAsync("genre", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await HandleApiErrorsAsync(response);
            return View(dto);
        }

        public async Task<IActionResult> RemoveGenres(int id)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.DeleteAsync($"genre/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }
        private async Task HandleApiErrorsAsync(HttpResponseMessage response)
        {
            try
            {
                var errorResult = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

                if (errorResult != null)
                {
                    if (!string.IsNullOrEmpty(errorResult.Message))
                    {
                        ModelState.AddModelError(string.Empty, errorResult.Message);
                    }

                    if (errorResult.Errors != null && errorResult.Errors.Any())
                    {
                        foreach (var error in errorResult.Errors)
                        {
                            string key = error.Code ?? string.Empty;
                            string message = error.ErrorMessage ?? "Bir hata oluştu.";

                            ModelState.AddModelError(key, message);
                        }
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Sunucudan gelen yanıt ayrıştırılamadı. Lütfen tekrar deneyin.");
            }
        }
    }
}