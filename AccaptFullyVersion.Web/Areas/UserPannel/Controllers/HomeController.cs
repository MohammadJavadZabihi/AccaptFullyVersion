using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using AccaptFullyVersion.DataLayer.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;

namespace AccaptFullyVersion.Web.Areas.UserPannel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IApiCallServies _callapiServies;
        public HomeController(IApiCallServies apiCallServies)
        {
            _callapiServies = apiCallServies ?? throw new ArgumentException(nameof(apiCallServies));
        }

        public async Task<IActionResult> Index()
        {
            var data = new
            {
                UserName = User.Identity.Name
            };

            var responseMessage = await _callapiServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/GUBN(V1)", data);

            if(responseMessage.IsSuccessStatusCode)
            {
                var respons = await responseMessage.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<InformationUserViewModel>(respons);

                return View();
            }

            return View();
        }
    }
}
