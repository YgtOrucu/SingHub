using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class IdentityVerificationStatus : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminDashboardComponents/IdentityVerificationStatus.cshtml");
        }
    }
}
