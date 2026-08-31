using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.PresantationComponents
{
    public class PresantationSidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/PresantationComponents/PresantationSidebar.cshtml");
        }
    }
}
