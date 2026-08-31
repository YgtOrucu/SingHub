using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.Controllers
{
    public class PresantationLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

