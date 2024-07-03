using Microsoft.AspNetCore.Mvc;

namespace AccaptFullyVersion.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
