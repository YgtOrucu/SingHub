using Microsoft.AspNetCore.Mvc;
using SingHub.Dto.ForPresantationPageDtos.NavbarDto;

namespace SingHub.WebUI.ViewComponents.PresantationComponents
{
    public class PresantationNavbar : ViewComponent
    {
        public IViewComponentResult Invoke(CheckLoginUser checkLogin)
        {
            return View("~/Views/Shared/Components/PresantationComponents/PresantationNavbar.cshtml", checkLogin);
        }
    }
}
