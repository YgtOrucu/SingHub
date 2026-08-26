using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.SongsDto;

namespace SingHub.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class SongsController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SingHubAPI");

    [HttpGet]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
    {
        var response = await _httpClient.GetAsync("song");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<BaseResult<List<ResultSongsDto>>>();
            var allData = result?.Data ?? new List<ResultSongsDto>();

            int totalCount = allData.Count(x => !x.IsDeleted);
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            string totalListenCount = allData.Where(x => !x.IsDeleted).Sum(x => x.ListenCount).ToString("N0");

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
            ViewBag.TotalListenCount = totalListenCount;

            return View(pagedData);
        }

        return View(new List<ResultSongsDto>());
    }

    [HttpGet]
    public async Task<IActionResult> CreateSongs()
    {
        await LoadViewBagsAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSongs(CreateSongsDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("song", dto);
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        await LoadViewBagsAsync();
        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateSongs(int id)
    {
        await LoadViewBagsAsync();
        var response = await _httpClient.GetAsync($"song/{id}");
        if (response.IsSuccessStatusCode)
        {
            var value = await response.Content.ReadFromJsonAsync<BaseResult<UpdateSongsDto>>();
            return View(value!.Data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateSongs(UpdateSongsDto dto)
    {
        var response = await _httpClient.PutAsJsonAsync("song", dto);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        await LoadViewBagsAsync();
        return View(dto);
    }

    public async Task<IActionResult> RemoveSongs(int id)
    {
        var response = await _httpClient.DeleteAsync($"song/{id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction("Index");

        return View();
    }

    private async Task LoadViewBagsAsync()
    {
        ViewBag.Artists = await GetLookupAsync<GetArtistForUpsertDto>("song/GetArtistForUpsert");
        ViewBag.Genres = await GetLookupAsync<GetGenreForUpsertDto>("song/GetGenreForUpsert");
        ViewBag.Albums = await GetLookupAsync<GetAlbumForUpsertDto>("song/GetAlbumForUpsert");
        ViewBag.Roles = await GetLookupAsync<GetRolesForUpsertDto>("song/GetRoleForUpsert");
    }

    private async Task<List<T>> GetLookupAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<BaseResult<List<T>>>();
            return result?.Data ?? new List<T>();
        }
        return new List<T>();
    }
}