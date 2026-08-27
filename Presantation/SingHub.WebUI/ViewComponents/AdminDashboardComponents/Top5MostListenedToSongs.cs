using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.ViewComponents.AdminComponents
{
    public class Top5MostListenedToSongs : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/AdminDashboardComponents/Top5MostListenedToSongs.cshtml");
        }
    }
}
