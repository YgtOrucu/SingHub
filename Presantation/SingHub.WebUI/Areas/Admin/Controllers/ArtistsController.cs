using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.ArtistsDto;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArtistsController(IHttpClientFactory httpClientFactory) : AdminBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.GetAsync("artist");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<BaseResult<List<ResultArtistsDto>>>();
                var allData = result?.Data ?? new List<ResultArtistsDto>();

                int totalCount = allData.Count;
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                if (page < 1) page = 1;
                if (page > totalPages && totalPages > 0) page = totalPages;

                var pagedData = allData
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalCount = totalCount;

                return View(pagedData);
            }
            var errorResult = await response.Content.ReadFromJsonAsync<BaseResult<ApiResponseError>>();
            ViewData["ErrorMessage"] = errorResult?.Message ?? "You do not have permission to access";
            return View(new List<ResultArtistsDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreateArtists()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtists(CreateArtistsDto dto)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.PostAsJsonAsync("artist", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await HandleApiErrorsAsync(response);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateArtists(int id)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.GetAsync($"artist/{id}");
            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<BaseResult<UpdateArtistsDto>>();
                return View(value!.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArtists(UpdateArtistsDto dto)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.PutAsJsonAsync("artist", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await HandleApiErrorsAsync(response);
            return View(dto);
        }

        public async Task<IActionResult> RemoveArtists(int id)
        {
            var client = httpClientFactory.CreateClient("SingHubAPI");
            var response = await client.DeleteAsync($"artist/{id}");
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