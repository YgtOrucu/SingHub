using Microsoft.AspNetCore.Mvc;
namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class AdminNavbar() : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.userName = HttpContext.User.FindFirst("FullName")?.Value;
            ViewBag.userRole = HttpContext.User.FindFirst("role")?.Value;
            ViewBag.FirstAndLastLetter = HttpContext.User.FindFirst("FirstAndLastLetter")?.Value;
            return View("~/Views/Shared/Components/AdminComponents/AdminNavbar.cshtml");
        }
    }
}
