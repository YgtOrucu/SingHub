using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Users.Result;
using SingHub.Dto.ForAdminPageDtos.UsersDtos;
using SingHub.WebUI.Models;

namespace SingHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SingHubAPI");

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usersResponse = await _httpClient.GetAsync("users/GetAllUsers");
            if (usersResponse.IsSuccessStatusCode)
            {
                var usersResult = await usersResponse.Content.ReadFromJsonAsync<BaseResult<List<GetListUsersDto>>>();
                ViewBag.Roles = await GetAllRoles();       
                return View(usersResult?.Data);
            }

            await GetErrors(usersResponse);
            return View();
        }

        private async Task<dynamic> GetAllRoles()
        {
            var rolesResponse = await _httpClient.GetAsync("users/GetAllRoles");
            var rolesResult = await rolesResponse.Content.ReadFromJsonAsync<BaseResult<List<GetAllRoleNameQueryResult>>>();
            return rolesResult?.Data;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(string userId, string roleName)
        {
            var updateDto = new { RoleName = roleName };

            var response = await _httpClient.PutAsJsonAsync($"users/UpdateUsersRoleName/{userId}", updateDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kullanıcı rolü başarıyla güncellendi.";
            }
            else
            {
                TempData["ErrorMessage"] =  "Rol güncellenirken bir hata oluştu.";
            }

            return RedirectToAction(nameof(Index));
        }

        #region GetErrorsMethod
        private async Task GetErrors(HttpResponseMessage response)
        {
            var result = await response.Content.ReadFromJsonAsync<BaseResult<ApiResponseError>>();
            if (result?.Errors != null)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.ErrorMessage ?? "An error occurred.");
                }
            }
        }
        #endregion
    }
}