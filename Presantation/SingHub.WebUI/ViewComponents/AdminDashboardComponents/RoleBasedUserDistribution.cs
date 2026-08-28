using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.DashboardDto.GetRoleBasedUserDistributionDto;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class RoleBasedUserDistribution(IHttpClientFactory _httpClientFactory) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("SingHubAPI");
            var responseMessage = await client.GetAsync("dashboard/RoleBasedUserDistribution");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<BaseResult<List<ResultGetRoleBasedUserDistribution>>>();
                return View("~/Views/Shared/Components/AdminDashboardComponents/RoleBasedUserDistribution.cshtml", values!.Data);
            }

            return View("~/Views/Shared/Components/AdminDashboardComponents/RoleBasedUserDistribution.cshtml", new List<ResultGetRoleBasedUserDistribution>());
        }
    }
}
