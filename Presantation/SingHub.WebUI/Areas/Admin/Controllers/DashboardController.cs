using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
