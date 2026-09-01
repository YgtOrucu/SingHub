using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.AlbumsDto;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlbumsController(IHttpClientFactory httpClientFactory) : AdminBaseController
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SingHubAPI");

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync("album");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<BaseResult<List<ResultAlbumsDto>>>();
                var allData = result?.Data ?? new List<ResultAlbumsDto>();

                int totalCount = allData.Where(x => !x.IsDeleted).Count();
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
            return View(new List<ResultAlbumsDto>());
        }

        [HttpGet]
        public async Task<IActionResult> CreateAlbums()
        {
            ViewBag.Artist = await GetArtist();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbums(CreateAlbumsDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("album", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await HandleApiErrorsAsync(response);

            ViewBag.Artist = await GetArtist();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAlbums(int id)
        {
            ViewBag.Artist = await GetArtist();
            var response = await _httpClient.GetAsync($"album/{id}");
            if (response.IsSuccessStatusCode)
            {
                var value = await response.Content.ReadFromJsonAsync<BaseResult<UpdateAlbumsDto>>();
                return View(value!.Data);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAlbums(UpdateAlbumsDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("album", dto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await HandleApiErrorsAsync(response);

            ViewBag.Artist = await GetArtist();
            return View(dto);
        }

        public async Task<IActionResult> RemoveAlbums(int id)
        {
            var response = await _httpClient.DeleteAsync($"album/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View();
        }

        private async Task<dynamic> GetArtist()
        {
            var response = await _httpClient.GetAsync("album/GetArtistForUpsert");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<BaseResult<List<GetArtistForUpsertDto>>>();
                return result?.Data ?? new List<GetArtistForUpsertDto>();
            }
            return new List<GetArtistForUpsertDto>();
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
                ModelState.AddModelError(string.Empty, "Sunucudan gelen yanıt okunamadı. Lütfen tekrar deneyin.");
            }
        }
    }
}