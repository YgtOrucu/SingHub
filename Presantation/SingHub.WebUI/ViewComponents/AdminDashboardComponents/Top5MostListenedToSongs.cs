using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.DashboardDto.Top5MostListenedToSongsDto;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class Top5MostListenedToSongs(IHttpClientFactory _httpClientFactory) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("SingHubAPI");

            var responseMessage = await client.GetAsync("dashboard/Top5MostListenedToSongs");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<BaseResult<List<ResultTop5MostListenedToSongs>>>();
                return View("~/Views/Shared/Components/AdminDashboardComponents/Top5MostListenedToSongs.cshtml", values!.Data);
            }

            return View("~/Views/Shared/Components/AdminDashboardComponents/Top5MostListenedToSongs.cshtml", new List<ResultTop5MostListenedToSongs>());
        }
    }
}
