using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.DashboardDto.StatGridCardDto;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class StatGridCard(IHttpClientFactory _httpClientFactory) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("SingHubAPI");

            var responseMessage = await client.GetAsync("dashboard/StatGridCard");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<BaseResult<ResultStatGridCard>>();
                return View("~/Views/Shared/Components/AdminDashboardComponents/StatGridCard.cshtml", values!.Data);
            }

            return View("~/Views/Shared/Components/AdminDashboardComponents/StatGridCard.cshtml", new ResultStatGridCard());
        }
    }
}
