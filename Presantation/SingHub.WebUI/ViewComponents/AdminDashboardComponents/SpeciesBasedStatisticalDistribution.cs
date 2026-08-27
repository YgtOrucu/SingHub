using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class SpeciesBasedStatisticalDistribution : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminDashboardComponents/SpeciesBasedStatisticalDistribution.cshtml");
        }
    }
}
