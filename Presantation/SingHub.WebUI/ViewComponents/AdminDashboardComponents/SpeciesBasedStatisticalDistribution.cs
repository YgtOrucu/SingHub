using Microsoft.AspNetCore.Mvc;
using SingHub.Application.Bases;
using SingHub.Dto.ForAdminPageDtos.DashboardDto.SpeciesBasedStatisticalDistributionDto;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class SpeciesBasedStatisticalDistribution(IHttpClientFactory _httpClientFactory) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("SingHubAPI");

            var responseMessage = await client.GetAsync("dashboard/GenreStatisticalDistribution");

            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadFromJsonAsync<BaseResult<List<ResultSpeciesBasedStatisticalDistribution>>>();
                return View("~/Views/Shared/Components/AdminDashboardComponents/SpeciesBasedStatisticalDistribution.cshtml", values!.Data);
            }

            return View("~/Views/Shared/Components/AdminDashboardComponents/SpeciesBasedStatisticalDistribution.cshtml", new List<ResultSpeciesBasedStatisticalDistribution>());
        }
    }
}
