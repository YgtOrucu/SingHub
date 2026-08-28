using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.DashboardDto.IdentityVerificationStatusDto;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class IdentityVerificationStatus(IHttpClientFactory _httpClientFactory) : ViewComponent
    {
       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("SingHubAPI");
            var responseMessage = await client.GetAsync("dashboard/IdentityVerificationStatus");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<BaseResult<ResultIdentityVerificationStatus>>();
                return View("~/Views/Shared/Components/AdminDashboardComponents/IdentityVerificationStatus.cshtml", values!.Data);
            }

            return View("~/Views/Shared/Components/AdminDashboardComponents/IdentityVerificationStatus.cshtml", new ResultIdentityVerificationStatus());
        }
    }
}
