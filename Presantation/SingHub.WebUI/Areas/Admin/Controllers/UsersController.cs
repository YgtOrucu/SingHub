using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.UsersDtos;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SingHubAPI");
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("users/GetAllUsers");
            if (response.IsSuccessStatusCode)
            {
                var values = await response.Content.ReadFromJsonAsync<BaseResult<List<GetListUsersDto>>>();
                return View(values?.Data);
            }
            await GetErrors(response);
            return View();
        }

        #region GetErrorsMethod
        private async Task GetErrors(HttpResponseMessage response)
        {
            var x = await response.Content.ReadAsStringAsync();
            var result = await response.Content.ReadFromJsonAsync<ApiResponseError>();
            if (result?.Errors != null)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Message ?? "An error occurred.");
                }
            }
        }

        #endregion
    }
}
