using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AccaptFullyVersion.Web.Areas.UserPannel.Controllers
{
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
