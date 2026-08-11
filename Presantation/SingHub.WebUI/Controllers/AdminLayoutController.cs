using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
