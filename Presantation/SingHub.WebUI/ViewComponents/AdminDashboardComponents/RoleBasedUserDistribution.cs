using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class RoleBasedUserDistribution : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminDashboardComponents/RoleBasedUserDistribution.cshtml");
        }
    }
}
