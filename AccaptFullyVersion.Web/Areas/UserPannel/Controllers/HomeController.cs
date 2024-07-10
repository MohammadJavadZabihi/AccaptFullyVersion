using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccaptFullyVersion.Web.Areas.UserPannel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var data = new
            {
                User.Identity.Name
            };

            return View();
        }
    }
}
