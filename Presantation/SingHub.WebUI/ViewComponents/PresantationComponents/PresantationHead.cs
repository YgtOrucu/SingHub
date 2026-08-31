using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.PresantationComponents
{
    public class PresantationHead : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/PresantationComponents/PresantationHead.cshtml");
        }
    }
}
