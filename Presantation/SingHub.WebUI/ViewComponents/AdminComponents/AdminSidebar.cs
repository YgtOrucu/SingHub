using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class AdminSidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminComponents/AdminSidebar.cshtml");
        }
    }
}
