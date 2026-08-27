using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SingHub.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminBaseController : Controller { }
}
