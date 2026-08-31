using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.PresantationComponents
{
    public class PresantationScript : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/PresantationComponents/PresantationScript.cshtml");
        }
    }
}
